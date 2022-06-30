using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class ProdutoVendidoDto
    {
        [JsonProperty(PropertyName = "id")]
        [Required]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "qtdVendida")]
        [Required]
        public int QtdVendida { get; set; }
    }
}
