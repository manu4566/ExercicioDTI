using Modelo.Domain.Models;
using Newtonsoft.Json;

namespace Modelo.Application.DTO
{
    public class MensagemRetornoAcaoUsuario
    {
        [JsonProperty("cadastroRealizado")]
        public bool CadastroRealizado { get; set; }

        [JsonProperty("usuario")]
        public UsuarioDto UsuarioDto { get; set; }
    }
}
