using Modelo.Application.DTO;


namespace Modelo.Application.Interfaces
{
    public interface IProcessarMsgAcaoProdutoAppService
    {
        Task<MensagemRetornoAcaoProduto> ProcessarMsgAcaoProduto(MensagemAcaoProduto msgProduto);
    }
}
