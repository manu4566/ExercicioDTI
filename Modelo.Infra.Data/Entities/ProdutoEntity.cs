using System;
using Microsoft.Azure.Cosmos.Table;
using Modelo.Domain.Entities;

namespace Modelo.Infra.Data.Mapping
{
    public class ProdutoEntity : TableEntity
    {
        public ProdutoEntity(Produto produto)
        {
            PartitionKey = produto.Nome;
            RowKey = produto.Id.ToString();

            Id = produto.Id.ToString();
            Nome = produto.Nome;
            Preco = produto.Preco;
            Descricao = produto.Descricao;
            QtdEstoque = produto.QtdEstoque;
        }
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
