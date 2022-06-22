using Modelo.Infra.Data.Entities;

namespace Modelo.Infra.Data.Interface
{
    public interface IProdutoRepository
    {
        public ProdutoEntity ObterProduto(string id);
        public void InserirProduto(ProdutoEntity produto);
        public void AtualizarProduto(ProdutoEntity produto);
        public List<ProdutoEntity> ObterTodosProdutos();
    }
}
