

using Modelo.Application.Enum;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class MensagemAcaoVenda
    {
        [JsonProperty(PropertyName = "acao")]
        [Required]
        public AcaoVenda Acao { get; set; }

        [JsonProperty(PropertyName = "venda")]       
        public VendaDto Venda { get; set; }

        [JsonProperty(PropertyName = "buscarId")]       
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "cpf")]    
        public string Cpf { get; set; }
    }
}
