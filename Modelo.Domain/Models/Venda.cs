

namespace Modelo.Domain.Models
{
    public class Venda
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CPF { get; set; }
        public List<ProdutoVendido> ProdutoVendidos { get; set; }
    }
}
