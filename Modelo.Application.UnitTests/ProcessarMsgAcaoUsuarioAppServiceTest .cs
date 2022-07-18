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
    public class ProcessarMsgAcaoUsuarioAppServiceTest
    {
        private IFixture _fixture;

        private Mock<ICadastrarUsuarioService> _cadastrarUsuarioService;
        private Mock<IConverterUsuario> _converterUsuario;

       [SetUp] 
        public void Setup()
        {
            _fixture = new Fixture();
            _cadastrarUsuarioService = new Mock<ICadastrarUsuarioService>();
            _converterUsuario = new Mock<IConverterUsuario>();
        }

        [Test] //Sempre colocar quando for teste 
        public void DeveCadastrarUsuarioQuandoAcaoForCadastrarRetornarSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoUsuario>()
                .With(msg => msg.Acao, AcaoUsuario.CadastrarUsuario)
                .Create();
            var usuario = _fixture.Create<Usuario>();
            var msgRetorno = AppConstantes.Api.Sucesso.Cadastro;

            _converterUsuario
             .Setup(mock => mock.UsuarioDtoParaUsuario(msgAcao.Usuario))
             .Returns(usuario)
             .Verifiable();

            _cadastrarUsuarioService
                .Setup(mock => mock.CadastrarUsuario(usuario))
                .ReturnsAsync(msgRetorno)
                .Verifiable();

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);

            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _cadastrarUsuarioService.Verify(mock => mock.CadastrarUsuario(usuario), Times.Once);
           _converterUsuario.Verify(mock => mock.UsuarioDtoParaUsuario(msgAcao.Usuario), Times.Once);
        }

        [Test] 
        public void DeveBuscarUsuarioQuandoAcaoForBuscarECpfForRegistradoERetornarUsuarioPreenchidoComSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoUsuario>()
                .With(msg => msg.Acao, AcaoUsuario.BuscarUsuario)
                .With(msg => msg.Cpf, "07335080657")
                .Create();          
            var usuario = _fixture.Create<Usuario>();
            var usuarioDto = _fixture.Create<UsuarioDto>();
            var msgRetorno = AppConstantes.Api.Sucesso.Busca;

            _cadastrarUsuarioService
               .Setup(mock => mock.BuscarUsuario(msgAcao.Cpf))
               .ReturnsAsync(usuario)
               .Verifiable();

            _converterUsuario
             .Setup(mock => mock.UsuarioParaUsuarioDto(usuario))
             .Returns(usuarioDto)
             .Verifiable();           

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);

            Assert.AreEqual(usuarioDto, retorno.Result.UsuarioDto);
            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _cadastrarUsuarioService.Verify(mock => mock.BuscarUsuario(msgAcao.Cpf), Times.Once);
            _converterUsuario.Verify(mock => mock.UsuarioParaUsuarioDto(usuario), Times.Once);
        }

        [Test]
        public void DeveBuscarUsuarioQuandoAcaoForBuscarECpfComPontoETracoForRegistradoERetornarUsuarioPreenchidoComSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoUsuario>()
                .With(msg => msg.Acao, AcaoUsuario.BuscarUsuario)
                .With(msg => msg.Cpf, "073.350.806-57")
                .Create();
            var cpfPadronizado = "07335080657";
            var usuario = _fixture.Create<Usuario>();
            var usuarioDto = _fixture.Create<UsuarioDto>();
            var msgRetorno = AppConstantes.Api.Sucesso.Busca;

            _cadastrarUsuarioService
               .Setup(mock => mock.BuscarUsuario(cpfPadronizado))
               .ReturnsAsync(usuario)
               .Verifiable();

            _converterUsuario
             .Setup(mock => mock.UsuarioParaUsuarioDto(usuario))
             .Returns(usuarioDto)
             .Verifiable();

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);

            Assert.AreEqual(usuarioDto, retorno.Result.UsuarioDto);
            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _cadastrarUsuarioService.Verify(mock => mock.BuscarUsuario(cpfPadronizado), Times.Once);
            _converterUsuario.Verify(mock => mock.UsuarioParaUsuarioDto(usuario), Times.Once);
        }

        [Test]
        public void NaoDeveBuscarUsuarioQuandoAcaoForBuscarECpfNaoForRegistradoERetornarMenssagemUsuarioNaoEncontrado()
        {
            var msgAcao = _fixture.Build<MensagemAcaoUsuario>()
                .With(msg => msg.Acao, AcaoUsuario.BuscarUsuario)                
                .Create();
            Usuario usuario = null;
            var msgRetorno = AppConstantes.Api.Erros.NaoEncontrado;

            _cadastrarUsuarioService
               .Setup(mock => mock.BuscarUsuario(msgAcao.Cpf))
               .ReturnsAsync(usuario)
               .Verifiable();            

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);
         
            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _cadastrarUsuarioService.Verify(mock => mock.BuscarUsuario(msgAcao.Cpf), Times.Once);          
        }


        private ProcessarMsgAcaoUsuarioAppService InstanciarProcessarMsgAcaoUsuarioAppService()
        {
            return new ProcessarMsgAcaoUsuarioAppService(
                    _cadastrarUsuarioService.Object,
                    _converterUsuario.Object
            );
        }
    }
}