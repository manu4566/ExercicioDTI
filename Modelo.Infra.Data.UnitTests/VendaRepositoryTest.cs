using AutoFixture;
using FluentAssertions;
using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using Modelo.Infra.Data.Repository;
using Moq;
using System.Text.Json;

namespace Modelo.Infra.Data.UnitTests
{
    public class VendaRepositoryTest
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
        public void InserirVenda()
        {
            _baseRepository
             .Setup(mock => mock.InserirEntidade(It.IsAny<VendaEntity>(), It.IsAny<string>()))
             .Verifiable();

            var appService = InstanciarVendaRepository();

            var retorno = appService.InserirVenda(_fixture.Create<Venda>());

            _baseRepository.Verify(mock => mock.InserirEntidade(It.IsAny<VendaEntity>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void BuscaVendaERetornaVenda()
        {
            var produtosVendidos = _fixture.Create<List<ProdutoVendido>>();
            string produtosVendidosJson = JsonSerializer.Serialize(produtosVendidos);

            var vendaEntity = _fixture.Build<VendaEntity>()
                .With(venda => venda.Id, Guid.Empty.ToString())
                .With(venda => venda.ProdutoVendidosJson, produtosVendidosJson)
                .Create();

            var vendasEntities = new List<VendaEntity>();
            vendasEntities.Add(vendaEntity);

            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesRowKeyAsync<VendaEntity>(It.IsAny<string>(), It.IsAny<string>()))
             .ReturnsAsync(vendasEntities);

            var appService = InstanciarVendaRepository();

            var retorno = appService.ObterVenda(vendaEntity.Id);

            retorno.Result.Id.Should().Be(Guid.Parse(vendaEntity.Id));           
            retorno.Result.Cpf.Should().Be(vendaEntity.CPF);

            for (int i = 0; i < produtosVendidos.Count; i++)
            {
                retorno.Result.ProdutosVendidos[i].Id.Should().Be(produtosVendidos[i].Id);
                retorno.Result.ProdutosVendidos[i].QtdVendida.Should().Be(produtosVendidos[i].QtdVendida);
            }
            
            //CollectionAssert.AreEquivalent(retorno.Result.ProdutosVendidos, produtosVendidos);
            //Assert.IsTrue(retorno.Result.ProdutosVendidos.SequenceEqual(produtosVendidos));
            //retorno.Result.ProdutosVendidos.Should().BeSameAs(produtosVendidos);
        }

        [Test]
        public void BuscaVendaERetornaNull()
        {  
            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesRowKeyAsync<VendaEntity>(It.IsAny<string>(), It.IsAny<string>()))
             .ReturnsAsync(new List<VendaEntity>());

            var appService = InstanciarVendaRepository();

            var retorno = appService.ObterVenda(It.IsAny<string>());

            retorno.Result.Should().BeNull();
        }

        [Test]
        public void BuscaTodasVendaPorCpfERetornaNull()
        {
            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesPartitionKeyAsync<VendaEntity>(It.IsAny<string>(), It.IsAny<string>()))
             .ReturnsAsync(new List<VendaEntity>());

            var appService = InstanciarVendaRepository();

            var retorno = appService.ObterTodasVendas(It.IsAny<string>());

            retorno.Result.Should().BeNull();
        }

        [Test]
        public void BuscaTodaVendaPorCpfERetornaListaDeVendas()
        {
            var produtosVendidos = _fixture.Create<List<ProdutoVendido>>();
            string produtosVendidosJson = JsonSerializer.Serialize(produtosVendidos);

            var vendaEntity = _fixture.Build<VendaEntity>()
                .With(venda => venda.Id, Guid.Empty.ToString())
                .With(venda => venda.ProdutoVendidosJson, produtosVendidosJson)
                .Create();

            var vendasEntities = new List<VendaEntity>();
            vendasEntities.Add(vendaEntity);

            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesPartitionKeyAsync<VendaEntity>(It.IsAny<string>(), It.IsAny<string>()))
             .ReturnsAsync(vendasEntities);

            var appService = InstanciarVendaRepository();

            var retorno = appService.ObterTodasVendas(It.IsAny<string>());

            retorno.Result[0].Id.Should().Be(vendasEntities[0].Id);
            retorno.Result[0].Cpf.Should().Be(vendasEntities[0].CPF);

            for (int i = 0; i < retorno.Result[0].ProdutosVendidos.Count; i++)
            {
                retorno.Result[0].ProdutosVendidos[i].Id.Should().Be(produtosVendidos[i].Id);
                retorno.Result[0].ProdutosVendidos[i].QtdVendida.Should().Be(produtosVendidos[i].QtdVendida);
            }

        }

        private VendaRepository InstanciarVendaRepository()
        {
            return new VendaRepository(
                  _baseRepository.Object
            );
        }
    }
}