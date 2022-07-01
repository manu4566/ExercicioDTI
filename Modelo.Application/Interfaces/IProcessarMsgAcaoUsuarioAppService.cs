using Modelo.Application.DTO;

namespace Modelo.Application.Interfaces
{
    public interface IProcessarMsgAcaoUsuarioAppService
    {
        Task<MensagemRetornoAcaoUsuario> ProcessarMsgAcaoUsuario(MensagemAcaoUsuario msgUsuario);
    }
}
