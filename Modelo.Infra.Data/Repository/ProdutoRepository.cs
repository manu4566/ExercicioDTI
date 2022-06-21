using Modelo.Domain.Entities;
using Modelo.Infra.Data.Interface;
using Modelo.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IBaseRepository _baseRepository;
        public ProdutoRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public void InserirProdutos(Produto produto)
        {
            ProdutoEntity produtoEntity = new ProdutoEntity(produto);
            _baseRepository.Insert(produtoEntity, typeof(ProdutoEntity).Name);

            throw new NotImplementedException();
        }

        public Produto ObterProdutos(string id)
        {
            throw new NotImplementedException();
        }
    }
}
