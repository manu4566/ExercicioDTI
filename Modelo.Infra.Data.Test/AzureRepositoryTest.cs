using AutoFixture;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Domain.Interfaces;
using Modelo.Infra.Data.Repository;
using Moq;
using NUnit.Framework;

namespace Modelo.Infra.Data.Test
{
    public class AzureRepositoryTest
    {
        private IFixture _fixture;  

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();         
        }

        [Test]
        public void DeveObterTabelaDoAzure()
        {            
            var azure = new AzureRepository();

            var retorno = azure.ObterTabela(It.IsAny<string>());

            retorno.Should().Be(It.IsAny<CloudTable>());
        }


    }


}