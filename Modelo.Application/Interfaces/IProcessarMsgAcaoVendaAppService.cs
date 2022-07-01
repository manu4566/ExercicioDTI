
using Modelo.Application.DTO;

namespace Modelo.Application.Interfaces
{
    public interface IProcessarMsgAcaoVendaAppService
    {
        Task<MensagemRetornoAcaoVenda> ProcessarMsgAcaoVenda(MensagemAcaoVenda msgAcaoVenda);
    }
}
