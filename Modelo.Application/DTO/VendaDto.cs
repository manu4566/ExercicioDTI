using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace Modelo.Application.DTO
{
    public class VendaDto
    {
        [JsonProperty(PropertyName = "cpf")]
        [Required]
        public string Cpf { get; set; }

        [JsonProperty(PropertyName = "produtosVendidos")]
        [Required]
        public List<ProdutoVendidoDto> ProdutosVendidos { get; set; }

    }
}
