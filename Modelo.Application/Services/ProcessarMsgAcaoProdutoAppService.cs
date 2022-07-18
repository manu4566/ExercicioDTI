using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Interfaces;
using Modelo.Share;
using System;


namespace Modelo.Application.Services
{
    public class ProcessarMsgAcaoProdutoAppService : IProcessarMsgAcaoProdutoAppService
    {
        private readonly ICadastrarProdutoService _cadastrarProdutoService;

        private readonly IConverterProduto _converterProduto;

        public ProcessarMsgAcaoProdutoAppService(
            ICadastrarProdutoService cadastrarProdutoService,
            IConverterProduto converterProduto)
        {
            _cadastrarProdutoService = cadastrarProdutoService;
            _converterProduto = converterProduto;
        }

        public Task<MensagemRetornoAcaoProduto> ProcessarMsgAcaoProduto(MensagemAcaoProduto msgProduto)
        {
            switch (msgProduto.Acao)
            {
                case Enum.AcaoProduto.CadastrarProduto:

                    return CadastrarProduto(msgProduto);

                case Enum.AcaoProduto.ObterProduto:

                    return BuscarProduto(msgProduto);

                case Enum.AcaoProduto.ObterTodosProdutos:

                    return BuscarTodosProdutos(msgProduto);             

                default:
                    return null;

            }
        }

        private async Task<MensagemRetornoAcaoProduto> CadastrarProduto(MensagemAcaoProduto msgProduto)
        {
            return new MensagemRetornoAcaoProduto
            {
                MensagemRetorno = await _cadastrarProdutoService.CadastrarProduto(_converterProduto.ProdutoDtoParaProduto(msgProduto.Produto))
            };
        }

        private async Task<MensagemRetornoAcaoProduto> BuscarProduto(MensagemAcaoProduto msgProduto)
        {
            var retorno = new MensagemRetornoAcaoProduto();
            var produtos = new List<ProdutoDto>();

            var produto = await _cadastrarProdutoService.ObterProduto(msgProduto.Id);          
            
            if (produto == null)
            {
                retorno.MensagemRetorno = AppConstantes.Api.Erros.NaoEncontrado;                
            }               
            else
            {              
                produtos.Add(_converterProduto.ProdutoParaProdutoDto(produto));

                retorno.MensagemRetorno = AppConstantes.Api.Sucesso.Busca;
                retorno.ProdutosDto = produtos;
            }

            return retorno;
        }

        private async Task<MensagemRetornoAcaoProduto> BuscarTodosProdutos(MensagemAcaoProduto msgProduto)
        {

            return new MensagemRetornoAcaoProduto
            {
                MensagemRetorno = AppConstantes.Api.Sucesso.Busca,
                ProdutosDto = _converterProduto.ProdutosParaProdutosDto(await _cadastrarProdutoService.ObterTodosProdutos())
            };
        }

      
    }
}
