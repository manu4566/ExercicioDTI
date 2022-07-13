using Modelo.Domain.Models;
using Newtonsoft.Json;

namespace Modelo.Application.DTO
{
    public class MensagemRetornoAcaoUsuario
    {
        [JsonProperty("mensagemRetorno")]
        public string MensagemRetorno { get; set; }

        [JsonProperty("usuario")]
        public UsuarioDto UsuarioDto { get; set; }
    }
}
