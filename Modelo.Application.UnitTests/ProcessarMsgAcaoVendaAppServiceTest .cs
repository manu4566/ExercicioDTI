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
    public class ProcessarMsgAcaoVendaAppServiceTest
    {
        private IFixture _fixture;

        private Mock<IVendaService> _realizarVendaService;
        private Mock<IConverterVenda> _converterVenda;

       [SetUp] 
        public void Setup()
        {
            _fixture = new Fixture();
            _realizarVendaService = new Mock<IVendaService>();
            _converterVenda = new Mock<IConverterVenda>();
        }

        [Test] //Sempre colocar quando for teste 
        public void DeveRealizarVendaQuandoAcaoForCadastrarVendaRetornarSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoVenda>()
                .With(msg => msg.Acao, AcaoVenda.CadastrarVenda)
                .Create();           
            var msgRetorno = AppConstantes.Api.Sucesso.Cadastro;

            _converterVenda
             .Setup(mock => mock.VendaDtoParaVenda(It.IsAny<VendaDto>()))
             .Returns(It.IsAny<Venda>());

            _realizarVendaService
                .Setup(mock => mock.CadastrarVenda(It.IsAny<Venda>()))
                .ReturnsAsync(msgRetorno);

            var appService = InstanciarProcessarMsgAcaoVendaAppService();

            var retorno = appService.ProcessarMsgAcaoVenda(msgAcao);

            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
           
        }

        [Test] 
        public void BuscarDetalhesDaVendaQuandoAcaoForBuscarComIdRegistradoERetornarDetalhesDaVenda()
        {
            var msgAcao = _fixture.Build<MensagemAcaoVenda>()
                .With(msg => msg.Acao, AcaoVenda.ObterDetalhesVenda)
                .Create();          
            var detalhesDaVenda = _fixture.Create<DetalhesVenda>();
            var detalhesDaVendaDto = _fixture.Create<DetalhesVendaDto>();

            _realizarVendaService
               .Setup(mock => mock.ObterDetalhesDaVenda(msgAcao.Id))
               .ReturnsAsync(detalhesDaVenda);

            _converterVenda
             .Setup(mock => mock.DetalhesVendaParaDetalhesVendaDto(detalhesDaVenda))
             .Returns(detalhesDaVendaDto);        

            var appService = InstanciarProcessarMsgAcaoVendaAppService();

            var retorno = appService.ProcessarMsgAcaoVenda(msgAcao);

            Assert.AreEqual(detalhesDaVendaDto, retorno.Result.DetalhesDaVendaDto);
            Assert.AreEqual(AppConstantes.Api.Sucesso.Busca, retorno.Result.MensagemRetorno);         
        }

        [Test]
        public void DeveBuscarDetalhesDaVendaQuandoAcaoForBuscarEIdNaoForRegistradoERetornarMensagemVendaNaoEncontrada()
        {
            var msgAcao = _fixture.Build<MensagemAcaoVenda>()
                .With(msg => msg.Acao, AcaoVenda.ObterDetalhesVenda)
                .Create();
           
            var msgRetorno = AppConstantes.Api.Erros.ObjetoNaoEncontrado;

            _realizarVendaService
               .Setup(mock => mock.ObterDetalhesDaVenda(msgAcao.Id));
           

            var appService = InstanciarProcessarMsgAcaoVendaAppService();

            var retorno = appService.ProcessarMsgAcaoVenda(msgAcao);

            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
        }

        [Test]
        public void BuscarTodasVendasPorCpfQuandoAcaoForBuscarTodasVendasERetornarListaDeVendas()
        {
            var msgAcao = _fixture.Build<MensagemAcaoVenda>()
                .With(msg => msg.Acao, AcaoVenda.ObterTodasVendas)
                .Create();
            var vendas = _fixture.Create<List<Venda>>();
            var vendasDto = _fixture.Create<List<VendaDto>>();
            var msgRetorno = AppConstantes.Api.Sucesso.Busca;

            _realizarVendaService
               .Setup(mock => mock.ObterTodasVendas(msgAcao.Cpf))
               .ReturnsAsync(vendas);

            _converterVenda
           .Setup(mock => mock.VendasParaVendasDto(vendas))
           .Returns(vendasDto);         

            var appService = InstanciarProcessarMsgAcaoVendaAppService();

            var retorno = appService.ProcessarMsgAcaoVenda(msgAcao);

            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            Assert.AreEqual(vendasDto, retorno.Result.VendasDto);

        }

        public void BuscarVendasPorCpfQuandoAcaoForBuscarTodasVendasENaoExistirVendasAssociadasERetornaMensagemVendaNaoEncontrada()
        {
            var msgAcao = _fixture.Build<MensagemAcaoVenda>()
                .With(msg => msg.Acao, AcaoVenda.ObterTodasVendas)
                .Create();      
           
            _realizarVendaService
               .Setup(mock => mock.ObterTodasVendas(msgAcao.Cpf));             

            var appService = InstanciarProcessarMsgAcaoVendaAppService();

            var retorno = appService.ProcessarMsgAcaoVenda(msgAcao);

            Assert.AreEqual(AppConstantes.Api.Erros.ObjetoNaoEncontrado, retorno.Result.MensagemRetorno);          

        }
        private ProcessarMsgAcaoVendaAppService InstanciarProcessarMsgAcaoVendaAppService()
        {
            return new ProcessarMsgAcaoVendaAppService(
                    _realizarVendaService.Object,
                    _converterVenda.Object
            );
        }
    }
}