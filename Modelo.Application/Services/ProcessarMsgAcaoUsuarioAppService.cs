

using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Validators;

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
                MensagemRetorno = await _cadastrarUsuarioService.CadastrarUsuario(_converterUsuario.UsuarioDto_Usuario(msgUsuario.Usuario))
            };
        }

        private async Task<MensagemRetornoAcaoUsuario> BuscarUsuario(MensagemAcaoUsuario msgUsuario)
        {
            var retorno = new MensagemRetornoAcaoUsuario();
            var cpf = CpfUteis.PadronizarCpf(msgUsuario.Cpf);

            var usuario = await _cadastrarUsuarioService.BuscarUsuario(cpf);

            if (usuario == null)
            {
                retorno.MensagemRetorno = "Usuario não encontrado.";
            }
            else
            {
                retorno.MensagemRetorno = "Usuario encontrado.";
                retorno.UsuarioDto = _converterUsuario.Usuario_UsuarioDto(usuario);
            }
            return retorno;
        }



    }
}
