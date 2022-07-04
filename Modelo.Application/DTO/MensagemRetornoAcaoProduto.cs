
using Newtonsoft.Json;

namespace Modelo.Application.DTO
{
    public class MensagemRetornoAcaoProduto
    {
        [JsonProperty("cadastroRealizado")]
        public bool CadastroRealizado { get; set; }

        [JsonProperty("produtos")]
        public List<ProdutoDto> ProdutosDto { get; set; }

        [JsonProperty("produto")]
        public ProdutoDto ProdutoDto { get; set; }
    }
}
