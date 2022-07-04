using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Models;

namespace Modelo.Application.Mapping
{
    public class ConverterProduto : IConverterProduto
    {
        public List<ProdutoDto> Produtos_ProdutosDto(List<Produto> produtos)
        {
            var produtosDto = new List<ProdutoDto>();

            foreach (var produto in produtos)
            {
                produtosDto.Add(Produto_ProdutoDto(produto));
            }
            return produtosDto;
        }

        public ProdutoDto Produto_ProdutoDto(Produto produto)
        {
            return new ProdutoDto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                Nome = produto.Nome,
                Preco = produto.Preco,
                QtdEstoque = produto.QtdEstoque

            };
        }

        public Produto ProdutoDto_Produto(ProdutoDto produtoDto)
        {
            return new Produto
            {
                Id = produtoDto.Id,
                Descricao = produtoDto.Descricao,
                Nome = produtoDto.Nome,
                Preco = produtoDto.Preco,
                QtdEstoque = produtoDto.QtdEstoque

            };
        }
    }
}
