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

        private Mock<IUsuarioRepository> _usuarioRepository;
       
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _usuarioRepository = new Mock<IUsuarioRepository>();
           
        }

        [Test] 
        public void CadastraUsuarioComCpfInvalidoERetornaMensagemDeErro()
        {
            var usuario = _fixture.Build<Usuario>()
                .With(usuario => usuario.Cpf, "70000000000")
                .Create();

            var appService = InstanciarUsuarioService();

            var retorno = appService.CadastrarUsuario(usuario);

            retorno.Result.Should().Be(AppConstantes.Api.Erros.Usuario.CpfInvalido);            
        }

        [Test]
        public void CadastraUsuarioComCpfValidoEDadosJaRegistradosERetornaMensagemDeErro()
        {
            var usuario = _fixture.Build<Usuario>()
                .With(usuario => usuario.Cpf, "542.208.500-07")
                .Create();

            _usuarioRepository
                .Setup(mock => mock.ConferirExistenciaDeCpfEEmail(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var appService = InstanciarUsuarioService();

            var retorno = appService.CadastrarUsuario(usuario);

            retorno.Result.Should().Be(AppConstantes.Api.Erros.Usuario.DadosInvalidos);
        }

        [Test]
        public void CadastraUsuarioComCpfValidoEDadosNaoRegistradosERetornaMensagemDeSucesso()
        {
            var usuario = _fixture.Build<Usuario>()
                .With(usuario => usuario.Cpf, "542.208.500-07")
                .Create();

            _usuarioRepository
                .Setup(mock => mock.ConferirExistenciaDeCpfEEmail(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            _usuarioRepository
                .Setup(mock => mock.InserirUsuario(It.IsAny<Usuario>()))
                .Verifiable();

            var appService = InstanciarUsuarioService();

            var retorno = appService.CadastrarUsuario(usuario);

            retorno.Result.Should().Be(AppConstantes.Api.Sucesso.Cadastro);
            _usuarioRepository.Verify(mock => mock.InserirUsuario(It.IsAny<Usuario>()), Times.Once);
        }

        [Test]
        public void BuscaUsuarioPorCpfERetornaUsuario()
        {
            var usuario = _fixture.Create<Usuario>();

            _usuarioRepository
             .Setup(mock => mock.ObterUsuarioPeloCpf(It.IsAny<string>()))
             .ReturnsAsync(usuario);

            var appService = InstanciarUsuarioService();

            var retorno = appService.BuscarUsuario(It.IsAny<string>());

            Assert.AreEqual(retorno.Result, usuario);

        }


        private UsuarioService InstanciarUsuarioService()
        {
            return new UsuarioService(
                    _usuarioRepository.Object                    
            );
        }
    }
}