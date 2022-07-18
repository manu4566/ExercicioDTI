using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Share;
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
        public async Task<string> CadastrarProduto(Produto produto)
        {
            try
            {                
                await _produtoRepository.InserirProduto(produto);

                return AppConstantes.Api.Sucesso.Cadastro;
            }
            catch(Exception ex)
            {
                throw ex;
            }      
        }

        public async Task<List<Produto>> ObterTodosProdutos()
        {
            try
            {
                return await _produtoRepository.ObterTodosProdutos();
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<Produto> ObterProduto(string id)
        {
            try
            {
                return await _produtoRepository.ObterProduto(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
