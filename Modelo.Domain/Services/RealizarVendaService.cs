using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Services
{
    public class RealizarVendaService : IRealizarVendaService
    {
        private readonly IProdutoRepository _produtoRepository;

        private readonly IVendasRepository _vendasRepository;
        public RealizarVendaService(IVendasRepository vendasRepository, IProdutoRepository produtoRepository)
        {
            _vendasRepository = vendasRepository;
            _produtoRepository = produtoRepository;
        }
        public async Task<bool> CadastrarVenda(Venda venda)
        {   
           
            var produtosVendaPermitida = await VerificarEstoque(venda.ProdutoVendidos);

            if(produtosVendaPermitida.Count == venda.ProdutoVendidos.Count)
            {
                await AtualizarEstoque(produtosVendaPermitida);
                return await _vendasRepository.InserirVenda(venda);
            }        
            
            return false;       
        }

        public async Task<Venda> ObterVenda(Guid id)
        {
           return await _vendasRepository.ObterVenda(id.ToString());
            //incluindo os itens, seus nomes, valores e o valor total da compra.

        }

        private async Task<List<Produto>> ObterProdutosDaVenda(Guid id)
        {
            var venda = await ObterVenda(id);
            if(venda != null)
            {
                var produtosEntity =  await _produtoRepository.ObterTodosProdutos();
                var produtosVendidos = new List<Produto>();

                foreach (var produtoVendido in venda.ProdutoVendidos)
                {
                    produtosVendidos.Add( produtosEntity.Where<Produto>(p => p.Id == produtoVendido.Id).First() );
                }

                return produtosVendidos;
            }

            return null;
        }

        private async Task<List<Produto>> VerificarEstoque(List<ProdutoVendido> produtosVendidos)
        {
            var produtosEntity = await _produtoRepository.ObterTodosProdutos();
            var produtosVendaPermitida = new List<Produto>();

            foreach (var produtoVendido in produtosVendidos)
            {
                var produtoEstoque = produtosEntity.Where(p => p.Id == produtoVendido.Id).First();           
                if (produtoEstoque.QtdEstoque >= produtoVendido.QtdVendida)
                {
                    produtosVendaPermitida.Add(produtoEstoque);
                }
            }

            return produtosVendaPermitida;
        }

        private async Task<bool> AtualizarEstoque(List<Produto> produtosVendaPermitida)
        {            
            return await _produtoRepository.AtualizarProdutos(produtosVendaPermitida);
        }

    }
}
