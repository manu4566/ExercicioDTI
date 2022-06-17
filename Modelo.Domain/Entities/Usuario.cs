

namespace Modelo.Domain.Entities
{
    public class Usuario
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Admin { get; set; } 
    }
}
