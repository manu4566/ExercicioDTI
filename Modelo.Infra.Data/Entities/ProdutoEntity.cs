using System;
using Microsoft.Azure.Cosmos.Table;


namespace Modelo.Infra.Data.Entities
{
    public class ProdutoEntity : TableEntity
    {
        public ProdutoEntity() { }
   
        public ProdutoEntity(string nome, Guid id, float preco, string descricao, int qtdEstoque)
        {
            PartitionKey = nome;
            RowKey = id.ToString();

            Id = id.ToString();
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            QtdEstoque = qtdEstoque;
        }

        public string Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public string Descricao { get; set; }
        public int QtdEstoque { get; set; }
    }
}
