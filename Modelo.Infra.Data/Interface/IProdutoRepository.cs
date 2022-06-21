using Modelo.Domain.Entities;
using Modelo.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Infra.Data.Interface
{
    public interface IProdutoRepository
    {
        public Produto ObterProdutos(string id);
        public void InserirProdutos(Produto produto);
    }
}
