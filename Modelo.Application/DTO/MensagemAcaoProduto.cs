using Modelo.Application.Enum;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Application.DTO
{
    public class MensagemAcaoProduto
    {
        [JsonProperty(PropertyName = "acao")]
        [Required]
        public AcaoProduto? Acao { get; set; }

        [JsonProperty(PropertyName = "produto")]
        public ProdutoDto Produto { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "erro")]
        public string Erro { get; set; }
    }
}
