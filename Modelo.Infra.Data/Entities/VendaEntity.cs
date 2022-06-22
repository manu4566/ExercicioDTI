using Microsoft.Azure.Cosmos.Table;
using System;


namespace Modelo.Infra.Data.Entities
{
    public class VendaEntity : TableEntity    {
        public VendaEntity() { }
        public VendaEntity( string cPF, Guid id, string produtoVendidosJson)
        {
            PartitionKey = cPF;
            RowKey = id.ToString();

            Id = id.ToString();
            CPF = cPF;
            ProdutoVendidosJson = produtoVendidosJson;
        }

        public string Id { get; set; }
        public string CPF { get; set; }
        string ProdutoVendidosJson { get; set; }
    }
}
