using Microsoft.Azure.Cosmos.Table;
using Modelo.Infra.Data.Interface;
using System;

namespace Modelo.Infra.Data.Repository
{
    public class AzureRepository : IAzureRepository
    {
        private string storegeConnectionString = "DefaultEndpointsProtocol=https;AccountName=manudemostorage01;AccountKey=Y3iCc3DTyw2pOwNy6Nukijc/WRCdXMSxBuO1zpGYwHzInqQzimbY8W1pG50Z4M8u2JLM1GsRp+H2+AStcgk+PQ==;EndpointSuffix=core.windows.net";
              
        public CloudTable CriarTabela(string nomeTabela)
        {
            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse(storegeConnectionString);

            CloudTableClient tableClient =  storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            CloudTable table = tableClient.GetTableReference(nomeTabela);            
            table.CreateIfNotExists();

            return table;
        }

        

    }
}
