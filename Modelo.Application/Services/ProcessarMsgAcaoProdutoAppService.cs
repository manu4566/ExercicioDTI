using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Interfaces;
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
                MensagemRetorno = await _cadastrarProdutoService.CadastrarProduto(_converterProduto.ProdutoDto_Produto(msgProduto.Produto))
            };
        }

        private async Task<MensagemRetornoAcaoProduto> BuscarProduto(MensagemAcaoProduto msgProduto)
        {
            var retorno = new MensagemRetornoAcaoProduto();           

            var produto = await _cadastrarProdutoService.ObterProduto(msgProduto.Id);          
            
            if (produto == null)
            {
                retorno.MensagemRetorno = "Produto não encontrado.";                
            }               
            else
            {
                retorno.MensagemRetorno = "Produto encontrado.";
                retorno.ProdutosDto.Add(_converterProduto.Produto_ProdutoDto(produto));
            }

            return retorno;
        }

        private async Task<MensagemRetornoAcaoProduto> BuscarTodosProdutos(MensagemAcaoProduto msgProduto)
        {

            return new MensagemRetornoAcaoProduto
            {
                MensagemRetorno = "Busca Efetuada",
                ProdutosDto = _converterProduto.Produtos_ProdutosDto(await _cadastrarProdutoService.ObterTodosProdutos())
            };
        }

      
    }
}
