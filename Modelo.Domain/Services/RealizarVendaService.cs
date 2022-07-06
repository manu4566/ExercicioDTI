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

        private readonly IVendaRepository _vendasRepository;
        public RealizarVendaService(IVendaRepository vendasRepository, IProdutoRepository produtoRepository)
        {
            _vendasRepository = vendasRepository;
            _produtoRepository = produtoRepository;
        }
        public async Task<bool> CadastrarVenda(Venda venda)
        {   
           
            var produtosVendaPermitida = await VerificarEstoque(venda.ProdutosVendidos);

            if(produtosVendaPermitida.Count == venda.ProdutosVendidos.Count)
            {
                await AtualizarEstoque(produtosVendaPermitida);
                return await _vendasRepository.InserirVenda(venda);
            }        
            
            return false;       
        }

        public async Task<DetalhesVenda> ObterDetalhesDaVenda(Guid id)
        {
            
            var venda = await ObterVenda(id);
            if(venda == null)
            {
                return null;
            }
           
            var produtos = await ObterProdutosDaVenda(venda);

            return new DetalhesVenda()
            { 
                Id = venda.Id,
                Cpf = venda.Cpf,
                ProdutosVendidos = produtos,
                ValorTotal = ObterValorTotalDaVenda(produtos)

            };

        }
        private async Task<Venda> ObterVenda(Guid id)
        {
           return await _vendasRepository.ObterVenda(id.ToString());
            
        }

        private async Task<List<Produto>> ObterProdutosDaVenda(Venda venda)
        {
            var produtosEntity = await _produtoRepository.ObterTodosProdutos();
            var produtosVendidos = new List<Produto>();

            foreach (var produtoVendido in venda.ProdutosVendidos)
            {
                produtosVendidos.Add(produtosEntity.Where<Produto>(p => p.Id == produtoVendido.Id).First());
            }

            return produtosVendidos;
        }

        private double ObterValorTotalDaVenda(List<Produto> produtosVendidos)
        {       
            double valorTotal = 0;

            foreach (var produtoVendido in produtosVendidos)
            {
                valorTotal += produtoVendido.Preco;
            }

            return valorTotal;
        }

        public async Task<List<Venda>> ObterTodasVendas(string cpf)
        {
            return await _vendasRepository.ObterTodasVendas(cpf);
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
