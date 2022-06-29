using Modelo.Domain.Models;

namespace Modelo.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        public Task<Produto> ObterProduto(string id);
        public Task<bool> InserirProduto(Produto produto);
        public Task<bool> AtualizarProduto(Produto produto);
        public Task<List<Produto>> ObterTodosProdutos();
        public Task<bool> AtualizarProdutos(List<Produto> produtos);
    }
}
