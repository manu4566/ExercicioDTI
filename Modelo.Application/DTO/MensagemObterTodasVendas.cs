using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace Modelo.Application.DTO
{
    public class MensagemObterTodasVendas
    {
        [JsonProperty(PropertyName = "cpf")]
        [Required]
        public string Cpf { get; set; }
    }
}
