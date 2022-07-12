using Modelo.Domain.Models;

namespace Modelo.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        public Task<Produto> ObterProduto(string id);
        public Task InserirProduto(Produto produto);
        public Task AtualizarProduto(Produto produto);
        public Task<List<Produto>> ObterTodosProdutos();
        public Task AtualizarProdutos(List<Produto> produtos);
    }
}
