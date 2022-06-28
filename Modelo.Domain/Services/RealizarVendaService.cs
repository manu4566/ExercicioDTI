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
            return await _vendasRepository.InserirVenda(venda);         
        }

        private async Task<bool> AtualizarEstoque(ProdutoVendido produtoVendido)
        {
            var produtoEstoque = await _produtoRepository.ObterProduto(produtoVendido.Id.ToString());

            if (produtoEstoque != null)
            {
               if(produtoEstoque.QtdEstoque >= produtoVendido.QtdVendida)
                {
                    produtoEstoque.QtdEstoque = produtoEstoque.QtdEstoque - produtoVendido.QtdVendida;

                    return await _produtoRepository.AtualizarProduto(produtoEstoque);                    
                }
            }

            return false;
        }

      

    }
}
