using Modelo.Domain.Models;
using Newtonsoft.Json;

namespace Modelo.Application.DTO
{
    public class MensagemRetornoAcaoVenda
    {
        [JsonProperty("cadastroRealizado")]
        public bool CadastroRealizado { get; set; }

        [JsonProperty("detalhes")]
        public DetalhesVendaDto DetalhesDaVendaDto { get; set; }

        [JsonProperty("vendas")]
        public List<VendaDto> VendasDto { get; set; }
        
    }
}
