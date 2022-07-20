using AutoFixture;
using Modelo.Application.DTO;
using Modelo.Application.Mapping;
using Modelo.Domain.Models;
using Moq;
using FluentAssertions;
using Modelo.Application.Interfaces;

namespace Modelo.Application.UnitTests
{
    public class ConverterVendaTest
    {
        private IFixture _fixture;
        private Mock<IConverterProduto> _converterProduto;

        [SetUp] 
        public void Setup()
        {
            _converterProduto = new Mock<IConverterProduto>();
            _fixture = new Fixture();           
        }                   

        [Test]
        public void DeveConverterVendaDtoParaVenda()
        {
            var vendaDto = _fixture.Build<VendaDto>()
                .With(venda => venda.Id, "3d2807f5-f210-4f78-90f1-146a22fc33d2")
                .Create();                  
            var converter = InstanciarConverterVenda();

            var retorno = converter.VendaDtoParaVenda(vendaDto);

            retorno.Cpf.Should().Be(vendaDto.Cpf);
            retorno.Id.Should().NotBe(Guid.Parse(vendaDto.Id));

            for (int i = 0; i < retorno.ProdutosVendidos.Count; i++)
            {
                retorno.ProdutosVendidos[i].Id.Should().Be(vendaDto.ProdutosVendidos[i].Id);
                retorno.ProdutosVendidos[i].QtdVendida.Should().Be(vendaDto.ProdutosVendidos[i].QtdVendida);
            }           
           
        }

        [Test]
        public void DeveConverterDetalhesVendaParaDetalhesVendaDto()
        {
            var detalhesVenda = _fixture.Create<DetalhesVenda>();           
            var produtosDto = _fixture.Create<List<ProdutoDto>>();

            _converterProduto
             .Setup(mock => mock.ProdutosParaProdutosDto(detalhesVenda.ProdutosVendidos))
             .Returns(produtosDto);

            var converter = InstanciarConverterVenda();
            var retorno = converter.DetalhesVendaParaDetalhesVendaDto(detalhesVenda);

            retorno.Cpf.Should().Be(detalhesVenda.Cpf);          
            retorno.Id.Should().Be(detalhesVenda.Id);          
            retorno.ValorTotal.Should().Be(detalhesVenda.ValorTotal);

            for (int i = 0; i < produtosDto.Count; i++)
            {
                retorno.ProdutoVendidosDto[i].Should().Be(produtosDto[i]);
            }         

        }

        [Test]
        public void DeveConverterListaDeVendaParaListaDeVendaDto()
        {           
            var vendas = _fixture.Create<List<Venda>>();           

            var converter = InstanciarConverterVenda();
            var retorno = converter.VendasParaVendasDto(vendas);
            
            retorno[0].Cpf.Should().Be(vendas[0].Cpf);
            retorno[0].Id.Should().Be(vendas[0].Id.ToString());

            for (int i = 0; i < retorno[0].ProdutosVendidos.Count; i++)
            {
                retorno[0].ProdutosVendidos[i].Id.Should().Be(vendas[0].ProdutosVendidos[i].Id);
                retorno[0].ProdutosVendidos[i].QtdVendida.Should().Be(vendas[0].ProdutosVendidos[i].QtdVendida);
            }

        }

        private ConverterVenda InstanciarConverterVenda()
        {
            return new ConverterVenda(                   
                    _converterProduto.Object
            );
        }


    }
}