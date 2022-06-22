using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;

namespace Modelo.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IBaseRepository _baseRepository;
        public ProdutoRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public void AtualizarProduto(ProdutoEntity produto)
        {
            throw new NotImplementedException();
        }

        public void InserirProduto(ProdutoEntity produtoEntity)
        {
            _baseRepository.Insert(produtoEntity, typeof(ProdutoEntity).Name);
           
        }

        public ProdutoEntity ObterProduto(string id)
        {
            ProdutoEntity produtoEntity = (ProdutoEntity) _baseRepository.Select(id.ToString());
                    
            return produtoEntity;

        }

        public List<ProdutoEntity> ObterTodosProdutos()
        {
            List<ProdutoEntity> produtosEntities = new List<ProdutoEntity>();

            produtosEntities = (List<ProdutoEntity>) _baseRepository.Select(typeof(ProdutoEntity).Name);

            return produtosEntities;
        }
    }
}
