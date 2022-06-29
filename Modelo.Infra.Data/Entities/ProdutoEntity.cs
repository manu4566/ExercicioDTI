using System;
using Microsoft.WindowsAzure.Storage.Table;


namespace Modelo.Infra.Data.Entities
{
    public class ProdutoEntity : TableEntity
    {         
        public string Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public string Descricao { get; set; }
        public int QtdEstoque { get; set; }
    }
}
