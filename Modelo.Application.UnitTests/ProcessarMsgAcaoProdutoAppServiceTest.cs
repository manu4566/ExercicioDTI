using AutoFixture;
using Modelo.Application.DTO;
using Modelo.Application.Enum;
using Modelo.Application.Interfaces;
using Modelo.Application.Services;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Share;
using Moq;


namespace Modelo.Application.UnitTests
{
    public class ProcessarMsgAcaoProdutoAppServiceTest
    {
        private IFixture _fixture;

        private Mock<IProdutoService> _cadastrarProdutoService;
        private Mock<IConverterProduto> _converterProduto;

       [SetUp] 
        public void Setup()
        {
            _fixture = new Fixture();
            _cadastrarProdutoService = new Mock<IProdutoService>();
            _converterProduto = new Mock<IConverterProduto>();
        }

        [Test] //Sempre colocar quando for teste 
        public void DeveCadastrarProdutoQuandoAcaoForCadastrarRetornarSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoProduto>()
                .With(msg => msg.Acao, AcaoProduto.CadastrarProduto)
                .Create();
            var produto = _fixture.Create<Produto>();
            var msgRetorno = AppConstantes.Api.Sucesso.Cadastro;

            _converterProduto
             .Setup(mock => mock.ProdutoDtoParaProduto(msgAcao.Produto))
             .Returns(produto)
             .Verifiable();

            _cadastrarProdutoService
                .Setup(mock => mock.CadastrarProduto(produto))
                .ReturnsAsync(msgRetorno)
                .Verifiable();

            var appService = InstanciarProcessarMsgAcaoProdutoAppService();

            var retorno = appService.ProcessarMsgAcaoProduto(msgAcao);

            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _cadastrarProdutoService.Verify(mock => mock.CadastrarProduto(produto), Times.Once);
           _converterProduto.Verify(mock => mock.ProdutoDtoParaProduto(msgAcao.Produto), Times.Once);
        }

        [Test] 
        public void DeveBuscarProdutoQuandoAcaoForBuscarEIdForRegistradoERetornarProdutoPreenchidoComSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoProduto>()
                .With(msg => msg.Acao, AcaoProduto.ObterProduto)
                .Create();          
            var produto = _fixture.Create<Produto>();
            var produtoDto = _fixture.Create<ProdutoDto>();
            var msgRetorno = AppConstantes.Api.Sucesso.Busca;

            _cadastrarProdutoService
               .Setup(mock => mock.ObterProduto(msgAcao.Id))
               .ReturnsAsync(produto)
               .Verifiable();

            _converterProduto
             .Setup(mock => mock.ProdutoParaProdutoDto(produto))
             .Returns(produtoDto)
             .Verifiable();           

            var appService = InstanciarProcessarMsgAcaoProdutoAppService();

            var retorno = appService.ProcessarMsgAcaoProduto(msgAcao);

            Assert.AreEqual(produtoDto, retorno.Result.ProdutosDto.First());
            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _cadastrarProdutoService.Verify(mock => mock.ObterProduto(msgAcao.Id), Times.Once);
            _converterProduto.Verify(mock => mock.ProdutoParaProdutoDto(produto), Times.Once);
        }

        [Test]
        public void DeveBuscarProdutoQuandoAcaoForBuscarEIdNaoForRegistradoERetornarMensagemProdutoNaoEncontrado()
        {
            var msgAcao = _fixture.Build<MensagemAcaoProduto>()
                .With(msg => msg.Acao, AcaoProduto.ObterProduto)
                .Create();
                      
            var msgRetorno = AppConstantes.Api.Erros.ObjetoNaoEncontrado;

            _cadastrarProdutoService
               .Setup(mock => mock.ObterProduto(msgAcao.Id))               
               .Verifiable();

            var appService = InstanciarProcessarMsgAcaoProdutoAppService();

            var retorno = appService.ProcessarMsgAcaoProduto(msgAcao);
          
            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _cadastrarProdutoService.Verify(mock => mock.ObterProduto(msgAcao.Id), Times.Once);
           
        }

        [Test]
        public void DeveBuscarTodosProdutoQuandoAcaoForBuscarTodosProdutosERetornarListaDeProdutosComSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoProduto>()
                .With(msg => msg.Acao, AcaoProduto.ObterTodosProdutos)
                .Create();
            var produtos = _fixture.Create<List<Produto>>();
            var produtosDto = _fixture.Create<List<ProdutoDto>>();
            var msgRetorno = AppConstantes.Api.Sucesso.Busca;

            _cadastrarProdutoService
               .Setup(mock => mock.ObterTodosProdutos())
               .ReturnsAsync(produtos);

            _converterProduto
           .Setup(mock => mock.ProdutosParaProdutosDto(produtos))
           .Returns(produtosDto);

            var appService = InstanciarProcessarMsgAcaoProdutoAppService();

            var retorno = appService.ProcessarMsgAcaoProduto(msgAcao);

            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            Assert.AreEqual(produtosDto, retorno.Result.ProdutosDto);

        }

        private ProcessarMsgAcaoProdutoAppService InstanciarProcessarMsgAcaoProdutoAppService()
        {
            return new ProcessarMsgAcaoProdutoAppService(
                    _cadastrarProdutoService.Object,
                    _converterProduto.Object
            );
        }
    }
}