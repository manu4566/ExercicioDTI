

namespace Modelo.Domain.Models
{
    public class Venda
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public List<ProdutoVendido> ProdutosVendidos { get; set; }      
    }
}
