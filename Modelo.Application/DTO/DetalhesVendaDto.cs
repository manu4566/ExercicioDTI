
using Newtonsoft.Json;

namespace Modelo.Application.DTO
{
    public class DetalhesVendaDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("produtosVendidos")]
        public List<ProdutoDto> ProdutoVendidosDto { get; set; }

        [JsonProperty("valorTotal")]
        public double ValorTotal { get; set; }
    }
}
