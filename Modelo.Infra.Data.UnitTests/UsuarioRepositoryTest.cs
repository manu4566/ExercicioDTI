using AutoFixture;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using Modelo.Infra.Data.Repository;
using Moq;

namespace Modelo.Infra.Data.UnitTests
{
    public class UsuarioRepositoryTest
    {
        private IFixture _fixture;

        private Mock<IBaseRepository> _baseRepository;
       
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _baseRepository = new Mock<IBaseRepository>();
           
        }       

        [Test]
        public void InserirUsuario()
        {
            _baseRepository
             .Setup(mock => mock.InserirEntidade(It.IsAny<UsuarioEntity>(), It.IsAny<string>()))
             .Verifiable();

            var appService = InstanciarUsuarioRepository();

            var retorno = appService.InserirUsuario(_fixture.Create<Usuario>());

            _baseRepository.Verify(mock => mock.InserirEntidade(It.IsAny<UsuarioEntity>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void BuscaUsuarioPorCpfERetornaUsuario()
        {
            var usuariosEntities = _fixture.Create<List<UsuarioEntity>>();

            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesPartitionKeyAsync<UsuarioEntity>(It.IsAny<string>(), It.IsAny<string>()))
             .ReturnsAsync(usuariosEntities);

            var appService = InstanciarUsuarioRepository();

            var retorno = appService.ObterUsuarioPeloCpf(usuariosEntities[0].CPF);

            retorno.Result.Cpf.Should().Be(usuariosEntities[0].CPF);
            retorno.Result.Nome.Should().Be(usuariosEntities[0].Nome);
            retorno.Result.Admin.Should().Be(usuariosEntities[0].Admin);
            retorno.Result.Senha.Should().Be(usuariosEntities[0].Senha);
            retorno.Result.Email.Should().Be(usuariosEntities[0].Email);

        }

        [Test]
        public void BuscaUsuarioERetornaNull()
        {           
            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesPartitionKeyAsync<UsuarioEntity>(It.IsAny<string>(), It.IsAny<string>()))
             .ReturnsAsync(new List<UsuarioEntity>());

            var appService = InstanciarUsuarioRepository();

            var retorno = appService.ObterUsuarioPeloCpf(It.IsAny<string>());

            retorno.Result.Should().BeNull();

        }

        [Test]
        public void ConfereExistenciaDeDadosRegistradosERetornaTrue()
        {            
            _baseRepository
             .Setup(mock => mock.BuscarEntidadesQueryAsync(It.IsAny<TableQuery<UsuarioEntity>>(), It.IsAny<string>()))
             .ReturnsAsync(_fixture.Create<List<UsuarioEntity>>());

            var appService = InstanciarUsuarioRepository();

            var retorno = appService.ConferirExistenciaDeCpfEEmail(It.IsAny<string>(), It.IsAny<string>());

            retorno.Result.Should().BeTrue();

        }

        [Test]
        public void ConfereExistenciaDeDadosRegistradosERetornaFalse()
        {          
            _baseRepository
             .Setup(mock => mock.BuscarEntidadesQueryAsync(It.IsAny<TableQuery<UsuarioEntity>>(), It.IsAny<string>()))
             .ReturnsAsync(new List<UsuarioEntity>());

            var appService = InstanciarUsuarioRepository();

            var retorno = appService.ConferirExistenciaDeCpfEEmail(It.IsAny<string>(), It.IsAny<string>());

            retorno.Result.Should().BeFalse();

        }

        [Test]
        public void ConfereExistenciaDeDadosRegistradosERetornaExcecao()
        {
            var exception = _fixture.Create<Exception>();
            var retornoExeption = new Exception();

            _baseRepository
              .Setup(mock => mock.BuscarEntidadesQueryAsync(It.IsAny<TableQuery<UsuarioEntity>>(), It.IsAny<string>()))
              .ThrowsAsync(exception);

            var appService = InstanciarUsuarioRepository();
            Func<Task> retorno = async () => await appService.ConferirExistenciaDeCpfEEmail(It.IsAny<string>(), It.IsAny<string>());
            retorno.Should().ThrowAsync<Exception>();           

        }

        [Test]
        public void InsereUsuariosERetornaExcecao()
        {
            var exception = _fixture.Create<Exception>();
            var retornoExeption = new Exception();

            _baseRepository
              .Setup(mock => mock.InserirEntidade(It.IsAny<UsuarioEntity>(), It.IsAny<string>()))
              .ThrowsAsync(exception)
              .Verifiable();

            var appService = InstanciarUsuarioRepository();
            Func<Task> retorno = async () => await appService.InserirUsuario(_fixture.Create<Usuario>());
            retorno.Should().ThrowAsync<Exception>();       
        }

        [Test]
        public void BuscaUsuarioERetornaExcecao()
        {
            var exception = _fixture.Create<Exception>();
            var retornoExeption = new Exception();
            
            _baseRepository
            .Setup(mock => mock.BuscarTodasEntidadesPartitionKeyAsync<UsuarioEntity>(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(exception);

            var appService = InstanciarUsuarioRepository();
            Func<Task> retorno = async () => await appService.ObterUsuarioPeloCpf(It.IsAny<string>());
            retorno.Should().ThrowAsync<Exception>();
        }    

        private UsuarioRepository InstanciarUsuarioRepository()
        {
            return new UsuarioRepository(
                  _baseRepository.Object
            );
        }
    }
}