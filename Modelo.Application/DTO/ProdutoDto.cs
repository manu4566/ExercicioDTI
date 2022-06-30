using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class ProdutoDto
    {
        [JsonProperty(PropertyName = "nome")]
        [Required]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "preco")]
        [Required]
        public float Preco { get; set; }

        [JsonProperty(PropertyName = "descricao")]
        [Required]
        public string Descricao { get; set; }

        [JsonProperty(PropertyName = "qtdEstoque")]
        [Required]
        public int QtdEstoque { get; set; }
    }
}
