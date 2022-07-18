using Modelo.Application.DTO;
using Modelo.Domain.Models;

namespace Modelo.Application.Interfaces
{
    public interface IConverterProduto
    {
        List<ProdutoDto> ProdutosParaProdutosDto(List<Produto> produtos);
        ProdutoDto ProdutoParaProdutoDto(Produto produto);
        Produto ProdutoDtoParaProduto(ProdutoDto produtoDto);

    }
}
