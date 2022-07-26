using FluentAssertions;
using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Infra.Data.Repository;
using Moq;

namespace Modelo.Infra.Data.UnitTests
{
    public class AzureRepositoryTest
    {
        [Test]
        public void DeveObterTabelaDoAzure()
        {
            var nomeTabela = "tabela";
            
            var azure = new AzureRepository();
            var retorno = azure.ObterTabela(nomeTabela);

            retorno.Should().BeOfType<CloudTable>();
            retorno.Name.Should().Be(nomeTabela);
        }
    }
}