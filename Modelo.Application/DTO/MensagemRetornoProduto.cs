
using Newtonsoft.Json;

namespace Modelo.Application.DTO
{
    public class MensagemRetornoProduto
    {
        [JsonProperty("cadastroRealizado")]
        public bool CadastroRealizado { get; set; }

        [JsonProperty("produtos")]
        public List<ProdutoDto> Produtos { get; set; }

        [JsonProperty("produto")]
        public ProdutoDto Produto { get; set; }
    }
}
