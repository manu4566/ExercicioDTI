using System;
using Microsoft.Azure.Cosmos.Table;


namespace Modelo.Infra.Data.Entities
{
    public class UsuarioEntity : TableEntity
    {
        public UsuarioEntity() { }
        public UsuarioEntity(string cPF, string nome, string email, string senha, bool admin)
        {
            PartitionKey = cPF;
            RowKey = email;

            CPF = cPF;
            Nome = nome;
            Email = email;
            Senha = senha;
            Admin = admin;
        }

        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Admin { get; set; }
    }
}
