using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Modelo.Infra.Data.Entities
{
    public class UsuarioEntity : TableEntity
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

       // [EncryptProperty]
        public string Senha { get; set; }
        public bool Admin { get; set; }
    }
}
