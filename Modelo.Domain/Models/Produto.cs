
namespace Modelo.Domain.Models
{
    public class Produto
    {
        public Guid Id { get; set; } //= Guid.NewGuid();
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public int Qtd { get; set; }

    }
}
