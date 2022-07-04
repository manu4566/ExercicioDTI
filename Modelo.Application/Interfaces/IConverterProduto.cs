using Modelo.Application.DTO;
using Modelo.Domain.Models;

namespace Modelo.Application.Interfaces
{
    public interface IConverterProduto
    {
        List<ProdutoDto> Produtos_ProdutosDto(List<Produto> produtos);
        ProdutoDto Produto_ProdutoDto(Produto produto);
        Produto ProdutoDto_Produto(ProdutoDto produtoDto);

    }
}
