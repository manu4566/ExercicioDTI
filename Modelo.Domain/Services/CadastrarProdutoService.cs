using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Services
{
    public class CadastrarProdutoService : ICadastrarProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public CadastrarProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task CadastrarProduto(Produto produto)
        {
            try
            {
                await _produtoRepository.InserirProduto(produto);
            }
            catch(Exception ex)
            {
                throw ex;
            }      
        }

        public async Task<List<Produto>> ObterTodosProdutos()
        {
            return await _produtoRepository.ObterTodosProdutos();
        }

        public async Task<Produto> ObterProduto(string id)
        {
            return await _produtoRepository.ObterProduto(id);
        }
    }
}
