

using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Interfaces;

namespace Modelo.Application.Services
{
    public class ProcessarMsgAcaoUsuarioAppService : IProcessarMsgAcaoUsuarioAppService
    {
        private readonly ICadastrarUsuarioService _cadastrarUsuarioService;

        private readonly IConverterUsuario _converterUsuario;

        public ProcessarMsgAcaoUsuarioAppService(
            ICadastrarUsuarioService cadastrarUsuarioService,
            IConverterUsuario converterUsuario)
        {
            _cadastrarUsuarioService = cadastrarUsuarioService;
            _converterUsuario = converterUsuario;   
        }

        public Task<MensagemRetornoAcaoUsuario> ProcessarMsgAcaoUsuario(MensagemAcaoUsuario msgUsuario)
        {
            switch (msgUsuario.Acao)
            {
                case Enum.AcaoUsuario.CadastrarUsuario:

                    return CadastrarUsuario(msgUsuario);

                case Enum.AcaoUsuario.BuscarUsuario:

                    return BuscarUsuario(msgUsuario);              

                default:
                    return null;

            }
        }

        private async Task<MensagemRetornoAcaoUsuario> CadastrarUsuario(MensagemAcaoUsuario msgUsuario)
        {
            return new MensagemRetornoAcaoUsuario
            {
                CadastroRealizado = await _cadastrarUsuarioService.CadastrarUsuario(_converterUsuario.UsuarioDto_Usuario(msgUsuario.Usuario))
            };
        }

        private async Task<MensagemRetornoAcaoUsuario> BuscarUsuario(MensagemAcaoUsuario msgUsuario)
        {
            return new MensagemRetornoAcaoUsuario
            {
                UsuarioDto = _converterUsuario.Usuario_UsuarioDto( await _cadastrarUsuarioService.BuscarUsuario(msgUsuario.Cpf))
            };
        }

    }
}
