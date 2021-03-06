using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class ProdutoDto
    {
        [JsonProperty(PropertyName = "id")]
     
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "nome")]
        [Required]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "preco")]
        [Required]
        public double Preco { get; set; }

        [JsonProperty(PropertyName = "descricao")]
        [Required]
        public string Descricao { get; set; }

        [JsonProperty(PropertyName = "qtd")]
        [Required]
        public int Qtd { get; set; }
    }
}
