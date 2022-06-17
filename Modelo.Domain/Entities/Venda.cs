

namespace Modelo.Domain.Entities
{
    public class Venda
    {
        public string CPF { get; set; }
        List<ProdutoVendido> ProdutoVendidos { get; set; }
    }
}
