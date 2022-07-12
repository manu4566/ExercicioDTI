

namespace Modelo.Domain.Models
{
    public class Usuario
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Admin { get; set; } //Penser sobre isso
        public string Erro { get; set; }
    }
}
