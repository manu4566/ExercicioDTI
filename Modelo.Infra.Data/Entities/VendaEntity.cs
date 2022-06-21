using Microsoft.Azure.Cosmos.Table;
using System;


namespace Modelo.Infra.Data.Mapping
{
    public class VendaEntity : TableEntity
    {
        //public VendaDB() { }
        public VendaEntity( string cPF, Guid id, string produtoVendidos)
        {
            PartitionKey = cPF;
            RowKey = id.ToString();

            Id = id.ToString();
            CPF = cPF;
            ProdutoVendidos = produtoVendidos;
        }

        public string Id { get; set; }
        public string CPF { get; set; }
        string ProdutoVendidos { get; set; }
    }
}
