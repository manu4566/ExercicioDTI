using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;

namespace Modelo.Infra.Data.Interface
{
    public interface IProdutoRepository
    {
        public Task<Produto> ObterProduto(string id);
        public bool InserirProduto(Produto produto);
        public bool AtualizarProduto(Produto produto);
        public Task<List<Produto>> ObterTodosProdutos();
    }
}
