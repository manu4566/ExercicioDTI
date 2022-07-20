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

        private Mock<IUsuarioService> _cadastrarUsuarioService;
        private Mock<IConverterUsuario> _converterUsuario;

       [SetUp] 
        public void Setup()
        {
            _fixture = new Fixture();
            _cadastrarUsuarioService = new Mock<IUsuarioService>();
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
             .Returns(usuario);

            _cadastrarUsuarioService
                .Setup(mock => mock.CadastrarUsuario(usuario))
                .ReturnsAsync(msgRetorno);

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);

            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);          
        }

        [Test] 
        public void DeveBuscarUsuarioQuandoAcaoForBuscarECpfForRegistradoERetornarUsuarioPreenchidoComSucesso()
        {
            var msgAcao = _fixture.Build<MensagemAcaoUsuario>()
                .With(msg => msg.Acao, AcaoUsuario.BuscarUsuario)
                .With(msg => msg.Cpf, "07335080657")
                .Create();               
            var usuarioDto = _fixture.Create<UsuarioDto>();
            var usuario = _fixture.Create<Usuario>();

            _cadastrarUsuarioService
               .Setup(mock => mock.BuscarUsuario(It.IsAny<string>()))
               .ReturnsAsync(usuario);
            //Perguntar ao Italo
            _converterUsuario
             .Setup(mock => mock.UsuarioParaUsuarioDto(It.IsAny<Usuario>()))
             .Returns(usuarioDto);          

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);

            Assert.AreEqual(usuarioDto, retorno.Result.UsuarioDto);
            Assert.AreEqual(AppConstantes.Api.Sucesso.Busca, retorno.Result.MensagemRetorno);
            
        }

        [Test]
        public void BuscarUsuarioQuandoAcaoForBuscarComCpfPontuadoRegistradoERetornarUsuarioPreenchido()
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
               .ReturnsAsync(usuario);

            _converterUsuario
             .Setup(mock => mock.UsuarioParaUsuarioDto(usuario))
             .Returns(usuarioDto);

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);

            Assert.AreEqual(usuarioDto, retorno.Result.UsuarioDto);
            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);          
        }

        [Test]
        public void BuscaUsuarioQuandoAcaoForBuscarComCpfNaoRegistradoERetornaMensagemUsuarioNaoEncontrado()
        {
            var msgAcao = _fixture.Build<MensagemAcaoUsuario>()
                .With(msg => msg.Acao, AcaoUsuario.BuscarUsuario)
                .With(msg => msg.Cpf, "07335080657")
                .Create();
            
            var msgRetorno = AppConstantes.Api.Erros.ObjetoNaoEncontrado;

            _cadastrarUsuarioService
               .Setup(mock => mock.BuscarUsuario(msgAcao.Cpf));

            _converterUsuario
             .Setup(mock => mock.UsuarioParaUsuarioDto(It.IsAny<Usuario>()))
             .Verifiable();

            var appService = InstanciarProcessarMsgAcaoUsuarioAppService();

            var retorno = appService.ProcessarMsgAcaoUsuario(msgAcao);
         
            Assert.AreEqual(msgRetorno, retorno.Result.MensagemRetorno);
            _converterUsuario.Verify(mock => mock.UsuarioParaUsuarioDto(It.IsAny<Usuario>()), Times.Never);
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