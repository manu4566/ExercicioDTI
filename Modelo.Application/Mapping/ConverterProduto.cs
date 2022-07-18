using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Models;

namespace Modelo.Application.Mapping
{
    public class ConverterProduto : IConverterProduto
    {
        public List<ProdutoDto> ProdutosParaProdutosDto(List<Produto> produtos)
        {
            var produtosDto = new List<ProdutoDto>();

            foreach (var produto in produtos)
            {
                produtosDto.Add(ProdutoParaProdutoDto(produto));
            }
            return produtosDto;
        }

        public ProdutoDto ProdutoParaProdutoDto(Produto produto)
        {           

            return new ProdutoDto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Qtd = produto.Qtd

            };
        }

        public Produto ProdutoDtoParaProduto(ProdutoDto produtoDto)
        {
            return new Produto
            {
                Id = produtoDto.Id,
                Descricao = produtoDto.Descricao,
                Nome = produtoDto.Nome,
                Preco = produtoDto.Preco,
                Qtd = produtoDto.Qtd

            };
        }
    }
}
