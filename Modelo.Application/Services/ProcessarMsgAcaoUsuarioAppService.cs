

using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Validators;
using Modelo.Share;

namespace Modelo.Application.Services
{
    public class ProcessarMsgAcaoUsuarioAppService : IProcessarMsgAcaoUsuarioAppService
    {
        private readonly IUsuarioService _cadastrarUsuarioService;

        private readonly IConverterUsuario _converterUsuario;

        public ProcessarMsgAcaoUsuarioAppService(
            IUsuarioService cadastrarUsuarioService,
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
                MensagemRetorno = await _cadastrarUsuarioService.CadastrarUsuario(_converterUsuario.UsuarioDtoParaUsuario(msgUsuario.Usuario))
            };
        }

        private async Task<MensagemRetornoAcaoUsuario> BuscarUsuario(MensagemAcaoUsuario msgUsuario)
        {
            var retorno = new MensagemRetornoAcaoUsuario();
            var cpf = CpfUteis.PadronizarCpf(msgUsuario.Cpf);

            var usuario = await _cadastrarUsuarioService.BuscarUsuario(cpf);

            if (usuario == null)
            {
                retorno.MensagemRetorno = AppConstantes.Api.Erros.ObjetoNaoEncontrado;
            }
            else
            {
                retorno.MensagemRetorno = AppConstantes.Api.Sucesso.Busca;
                retorno.UsuarioDto = _converterUsuario.UsuarioParaUsuarioDto(usuario);
            }
            return retorno;
        }



    }
}
