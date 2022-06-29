using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace Modelo.Infra.Data.Entities
{
    public class VendaEntity : TableEntity   
    {       
        public string Id { get; set; }
        public string CPF { get; set; }
        public string ProdutoVendidosJson { get; set; }
    }
}
