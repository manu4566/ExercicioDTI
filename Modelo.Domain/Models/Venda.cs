

namespace Modelo.Domain.Entities
{
    public class Venda
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CPF { get; set; }
        List<ProdutoVendido> ProdutoVendidos { get; set; }
    }
}
