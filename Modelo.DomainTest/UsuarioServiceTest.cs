using AutoFixture;
using FluentAssertions;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Domain.Services;
using Modelo.Share;
using Moq;

namespace Modelo.Domain.UnitTests
{
    public class UsuarioServiceTest
    {
        private IFixture _fixture;

        private Mock<IProdutoRepository> _produtoRepository;
       
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _produtoRepository = new Mock<IProdutoRepository>();
           
        }

        [Test] 
        public void CadastraProdutoERetornaMensagemDeSucesso()
        {
            _produtoRepository
             .Setup(mock => mock.InserirProduto(It.IsAny<Produto>()))
             .Verifiable();          

            var appService = InstanciarProdutoService();

            var retorno = appService.CadastrarProduto(_fixture.Create<Produto>());

            retorno.Result.Should().Be( AppConstantes.Api.Sucesso.Cadastro );
            _produtoRepository.Verify(mock => mock.InserirProduto(It.IsAny<Produto>()), Times.Once);
        }

        [Test]
        public void BuscaTodosProdutosERetornaListaDeProdutosRegistrados()
        {
            var produtos = _fixture.CreateMany<Produto>().ToList(); 
           
            _produtoRepository
             .Setup(mock => mock.ObterTodosProdutos())
             .ReturnsAsync(produtos);

            var appService = InstanciarProdutoService();

            var retorno = appService.ObterTodosProdutos();

            Assert.AreEqual(retorno.Result, produtos);
        
        }

        [Test]
        public void BuscaProdutoPorIdERetornaProduto()
        {
            var produto = _fixture.Create<Produto>();

            _produtoRepository
             .Setup(mock => mock.ObterProduto(It.IsAny<string>()))
             .ReturnsAsync(produto);

            var appService = InstanciarProdutoService();

            var retorno = appService.ObterProduto(It.IsAny<string>());

            Assert.AreEqual(retorno.Result, produto);

        }


        private ProdutoService InstanciarProdutoService()
        {
            return new ProdutoService(
                    _produtoRepository.Object                    
            );
        }
    }
}