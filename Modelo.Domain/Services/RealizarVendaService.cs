using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;

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
        public async Task<string> CadastrarVenda(Venda venda)
        {
            try
            {
                string msg;
                var produtosVendaPermitida = await VerificarEstoque(venda.ProdutosVendidos);

                if (produtosVendaPermitida.Count.Equals(venda.ProdutosVendidos.Count))
                {
                    await AtualizarEstoque(produtosVendaPermitida);
                    await _vendasRepository.InserirVenda(venda);
                   
                    msg = "Cadastro realizado com sucesso";
                }
                else
                {
                    msg = "Erro: Pelo menos um produto não tem a quantidade solitada no estoque.";
                }

                return msg;
            }
            catch(Exception ex)
            {
                throw ex;
            }
                 
        }

        public async Task<DetalhesVenda> ObterDetalhesDaVenda(Guid id)
        {
            try
            {
                var venda = await ObterVenda(id);
                if (venda == null)
                {
                    return null;
                }

                var produtos = await ObterProdutosDaVenda(venda);

                return new DetalhesVenda()
                {
                    Id = venda.Id,
                    Cpf = venda.Cpf,
                    ProdutosVendidos = produtos,
                    ValorTotal = ObterValorTotalDaVenda(produtos, venda)

                };
            }
            catch(Exception ex)
            {
                throw ex;
            }          

        }
     
        private async Task<Venda> ObterVenda(Guid id)
        {
            try
            {
                return await _vendasRepository.ObterVenda(id.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }      
            
        }

        private async Task<List<Produto>> ObterProdutosDaVenda(Venda venda)
        {
            try
            {
                var produtosEntity = await _produtoRepository.ObterTodosProdutos();
                var produtosVendidos = new List<Produto>();

                foreach (var produtoVendido in venda.ProdutosVendidos)
                {
                    var produto = produtosEntity.Where<Produto>(p => p.Id == produtoVendido.Id).First();
                    produto.Qtd = produtoVendido.QtdVendida;

                    produtosVendidos.Add(produto);
                }

                return produtosVendidos;
            }
            catch(Exception ex)
            {
                throw ex;
            }          
        }

        private double ObterValorTotalDaVenda(List<Produto> produtos, Venda venda )
        {       
            double valorTotal = 0;

            foreach (var produto in produtos)
            {
                var produtoVendido = venda.ProdutosVendidos.Where<ProdutoVendido>(p=> p.Id == produto.Id).First();              
                valorTotal += produto.Preco * produtoVendido.QtdVendida;
            }

            return valorTotal;
        }

        public async Task<List<Venda>> ObterTodasVendas(string cpf)
        {
            try
            {
                return await _vendasRepository.ObterTodasVendas(cpf);
            }
            catch(Exception ex)
            {
                throw ex;
            }
         
        }

        private async Task<List<Produto>> VerificarEstoque(List<ProdutoVendido> produtosVendidos)
        {
            try
            {
                var produtosEntity = await _produtoRepository.ObterTodosProdutos();
                var produtosVendaPermitida = new List<Produto>();

                foreach (var produtoVendido in produtosVendidos)
                {
                    var produtoEstoque = produtosEntity.Where(p => p.Id == produtoVendido.Id).First();
                    if (produtoEstoque.Qtd >= produtoVendido.QtdVendida)
                    {
                        produtoEstoque.Qtd = produtoEstoque.Qtd - produtoVendido.QtdVendida;

                        produtosVendaPermitida.Add(produtoEstoque);
                    }
                }

                return produtosVendaPermitida;

            }
            catch(Exception ex)
            {
                throw ex;
            }
         
        }

        private async Task AtualizarEstoque(List<Produto> produtosVendaPermitida)
        {
            try
            {
                await _produtoRepository.AtualizarProdutos(produtosVendaPermitida);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        
    }
}
