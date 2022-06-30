using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class MensagemObterVenda
    {
        [JsonProperty(PropertyName = "id")]
        [Required]
        public Guid Id { get; set; }
    }
}
