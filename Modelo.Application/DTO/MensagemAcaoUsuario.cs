using Modelo.Application.Enum;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class MensagemAcaoUsuario
    {
        [JsonProperty(PropertyName = "acao")]
        [Required]
        public AcaoUsuario Acao { get; set; }

        [JsonProperty(PropertyName = "usuario")]
        public UsuarioDto Usuario { get; set; }                

        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
    }
}
