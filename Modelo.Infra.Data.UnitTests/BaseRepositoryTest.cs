using AutoFixture;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using Modelo.Infra.Data.Repository;
using Moq;
using System.Reflection;

namespace Modelo.Infra.Data.UnitTests
{
    public class BaseRepositoryTest
    {
        private IFixture _fixture;

        private Mock<IAzureRepository> _azureRepository;
        private Mock<CloudTable> _tableMock;

        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }
        [SetUp]
        public void SetupMocks()
        {
            var credentialsMock = new StorageCredentials( //Não precisa ser o nome ou a key corretos, pois estou mockando.
               "manudemostorage01",
               "Y3iCc3DTyw2pOwNy6Nukijc/WRCdXMSxBuO1zpGYwHzInqQzimbY8W1pG50Z4M8u2JLM1GsRp+H2+AStcgk+PQ==" 
            );
            var storageUriMock = new Uri("https://manudemostorage01.table.core.windows.net");  //Poderia ser qq uma Url, mas seria o link para levar a tabela no banco de dados
            _azureRepository = new Mock<IAzureRepository>();
            _tableMock = new Mock<CloudTable>(
              storageUriMock,
              credentialsMock
          );
        }      

        [Test]
        public void InsereEntidadeDeFormaAssincronaCorretamente()
        {
            var entidade = _fixture.Create<TableEntity>();
            var nomeTabela = _fixture.Create<string>();
            var tableResult = new TableResult();        

            _azureRepository
             .Setup(mock => mock.ObterTabela(nomeTabela))
             .Returns(_tableMock.Object)
             .Verifiable();

            _tableMock
               .Setup(table => table.ExecuteAsync(
                   It.Is<TableOperation>(
                        op =>
                            op.OperationType == TableOperationType.Insert
                            && op.Entity == entidade
                    )))
               .ReturnsAsync(tableResult)
               .Verifiable();

            //Não pode ser dessa forma, pois está executando a operação, enquanto It.Is<TableOperation> apenas referencia que será dessa forma.
            //_tableMock
            // .Setup(table => table.ExecuteAsync(TableOperation.Insert(entidade)))
            // .ReturnsAsync(tableResult)
            // .Verifiable();

            var appService = InstanciarBaseRepository();

            var retorno = appService.InserirEntidade(entidade,nomeTabela);
          
            retorno.Result.Should().Be(tableResult);
            _azureRepository.VerifyAll(); 
            _tableMock.VerifyAll();
        }

        [Test]
        public void AtualizaEntidadeDeFormaAssincronaCorretamente()
        {
            var entidade = _fixture.Create<TableEntity>();
            var nomeTabela = _fixture.Create<string>();
            var tableResult = new TableResult();

            _azureRepository
             .Setup(mock => mock.ObterTabela(nomeTabela))
             .Returns(_tableMock.Object)
             .Verifiable();

            _tableMock
               .Setup(table => table.ExecuteAsync(
                   It.Is<TableOperation>(
                        op =>
                            op.OperationType == TableOperationType.InsertOrMerge
                            && op.Entity == entidade
                    )))
               .ReturnsAsync(tableResult)
               .Verifiable();         

            var appService = InstanciarBaseRepository();

            var retorno = appService.AtualizarEntidade(entidade, nomeTabela);

            retorno.Result.Should().Be(tableResult);
            _azureRepository.VerifyAll();
            _tableMock.VerifyAll();
        }

        [Test]
        public void BuscaEntidadeDeFormaAssincronaCorretamente()
        {
            var entidade = _fixture.Create<TableEntity>();
            var nomeTabela = _fixture.Create<string>();
            var partitionKey = _fixture.Create<string>();
            var rowKey = _fixture.Create<string>();
            var tableResult = new TableResult
            { 
                Result = entidade
            };

            _azureRepository
             .Setup(mock => mock.ObterTabela(nomeTabela))
             .Returns(_tableMock.Object);

            _tableMock
               .Setup(table => table.ExecuteAsync(
                   It.Is<TableOperation>(
                        op =>
                            op.OperationType == TableOperationType.Retrieve                          
                    )))
               .ReturnsAsync(tableResult);

            var appService = InstanciarBaseRepository();

            var retorno = appService.BuscarEntidade<TableEntity>(partitionKey,rowKey, nomeTabela);

            retorno.Result.Should().Be(entidade);
        }


        [Test]
        public async Task BuscaTodasEntidadesPorPartitionKeyDeFormaAssincronaCorretamente()
        {
            var entidades = _fixture.CreateMany<TableEntity>().ToList();
            var nomeTabela = _fixture.Create<string>();
            var partitionKey = _fixture.Create<string>();

            _azureRepository
             .Setup(mock => mock.ObterTabela(nomeTabela))
             .Returns(_tableMock.Object);

            _tableMock
               .Setup(table => table.ExecuteQuerySegmentedAsync(
                    It.IsAny<TableQuery<TableEntity>>(),
                    It.IsAny<TableContinuationToken>()
                    ))
               .ReturnsAsync(MockarTableQuerySegment(entidades));

            var appService = InstanciarBaseRepository();

            var retorno = await appService.BuscarTodasEntidadesPartitionKeyAsync<TableEntity>(partitionKey, nomeTabela);

            retorno.Should().BeEquivalentTo(entidades);           
          
        }


        [Test]
        public async Task BuscaTodasEntidadesPorRowKeyDeFormaAssincronaCorretamente()
        {
            var entidades = _fixture.CreateMany<TableEntity>().ToList();
            var nomeTabela = _fixture.Create<string>();
            var rowKey = _fixture.Create<string>();

            _azureRepository
             .Setup(mock => mock.ObterTabela(nomeTabela))
             .Returns(_tableMock.Object);

            _tableMock
               .Setup(table => table.ExecuteQuerySegmentedAsync(
                    It.IsAny<TableQuery<TableEntity>>(),
                    It.IsAny<TableContinuationToken>()
                    ))
               .ReturnsAsync(MockarTableQuerySegment(entidades));

            var appService = InstanciarBaseRepository();

            var retorno = await appService.BuscarTodasEntidadesRowKeyAsync<TableEntity>(rowKey, nomeTabela);

            retorno.Should().BeEquivalentTo(entidades);
        }

        [Test]
        public async Task BuscaTodasEntidadesPorQueryDeFormaAssincronaCorretamente()
        {
            var entidades = _fixture.CreateMany<TableEntity>().ToList();
            var nomeTabela = _fixture.Create<string>();
            var query = _fixture.Create<TableQuery<TableEntity>>();

            _azureRepository
             .Setup(mock => mock.ObterTabela(nomeTabela))
             .Returns(_tableMock.Object);

            _tableMock
               .Setup(table => table.ExecuteQuerySegmentedAsync(
                    query,
                    It.IsAny<TableContinuationToken>()
                    ))
               .ReturnsAsync(MockarTableQuerySegment(entidades));

            var appService = InstanciarBaseRepository();

            var retorno = await appService.BuscarEntidadesQueryAsync<TableEntity>(query, nomeTabela);

            retorno.Should().BeEquivalentTo(entidades);
        }

        [Test]
        public async Task BuscaTodasEntidadesDeFormaAssincronaCorretamente()
        {
            var entidades = _fixture.CreateMany<TableEntity>().ToList();
            var nomeTabela = _fixture.Create<string>();

            _azureRepository
             .Setup(mock => mock.ObterTabela(nomeTabela))
             .Returns(_tableMock.Object);

            _tableMock
               .Setup(table => table.ExecuteQuerySegmentedAsync(
                    It.IsAny<TableQuery<TableEntity>>(),
                    It.IsAny<TableContinuationToken>()
                    ))
               .ReturnsAsync(MockarTableQuerySegment(entidades));

            var appService = InstanciarBaseRepository();

            var retorno = await appService.BuscarTodasEntidadesAsync<TableEntity>(nomeTabela);

            retorno.Should().BeEquivalentTo(entidades);           
        }

        private TableQuerySegment<TableEntity> MockarTableQuerySegment<TableEntity>(List<TableEntity> results)
        {
            var ctor = Array.Find(
                typeof(TableQuerySegment<TableEntity>)
                    .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic),
                c => c.GetParameters().Length == 1
            );

            return ctor.Invoke(new object[] { results }) as TableQuerySegment<TableEntity>;
        }


        private BaseRepository InstanciarBaseRepository()
        {
            return new BaseRepository(
                  _azureRepository.Object
            );
        }
    }
}