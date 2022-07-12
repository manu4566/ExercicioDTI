using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface ICadastrarProdutoService
    {
        Task CadastrarProduto(Produto produto);

        Task<List<Produto>> ObterTodosProdutos();

        Task<Produto> ObterProduto(string id);
    }
}
