using AutoFixture;
using Modelo.Application.DTO;
using Modelo.Application.Enum;
using Modelo.Application.Mapping;
using Modelo.Domain.Models;
using Modelo.Share;
using Moq;
using FluentAssertions;

namespace Modelo.Application.UnitTests
{
    public class ConverterProdutoTest
    {
        private IFixture _fixture;
     
       [SetUp] 
        public void Setup()
        {
            _fixture = new Fixture();           
        }

        [Test] 
        public void DeveConverterProdutoParaProdutoDto()
        {
            var produto = _fixture.Create<Produto>();
          
            var converter = new ConverterProduto();
            var retorno = converter.ProdutoParaProdutoDto(produto);

            retorno.Descricao.Should().Be(produto.Descricao);
            retorno.Nome.Should().Be(produto.Nome);
            retorno.Preco.Should().Be(produto.Preco);
            retorno.Qtd.Should().Be(produto.Qtd);
            retorno.Id.Should().Be(produto.Id);
        }

        [Test]
        public void DeveConverterProdutoDtoParaProduto()
        {
            var produtoDto = _fixture.Create<ProdutoDto>();       
            var converter = new ConverterProduto();

            var retorno = converter.ProdutoDtoParaProduto(produtoDto);

            retorno.Descricao.Should().Be(produtoDto.Descricao);
            retorno.Nome.Should().Be(produtoDto.Nome);
            retorno.Preco.Should().Be(produtoDto.Preco);
            retorno.Qtd.Should().Be(produtoDto.Qtd);
            retorno.Id.Should().Be(produtoDto.Id);
        }


        [Test]
        public void DeveConverterListaDeProdutoParaListaDeProdutoDto()
        {
            var produtos = _fixture.CreateMany<Produto>().ToList();
            var converter = new ConverterProduto();

            var retorno = converter.ProdutosParaProdutosDto(produtos);

            for (int i = 0; i < produtos.Count; i++)
            {
                retorno[i].Descricao.Should().Be(produtos[i].Descricao);
                retorno[i].Nome.Should().Be(produtos[i].Nome);
                retorno[i].Preco.Should().Be(produtos[i].Preco);
                retorno[i].Qtd.Should().Be(produtos[i].Qtd);
                retorno[i].Id.Should().Be(produtos[i].Id);            
            }               
            
        }

     
    }
}