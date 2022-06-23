using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Infra.Data.Interface;
using System;

namespace Modelo.Infra.Data.Repository
{
    public class AzureRepository : IAzureRepository
    {
        private readonly string storegeConnectionString = "DefaultEndpointsProtocol=https;AccountName=manudemostorage01;AccountKey=Y3iCc3DTyw2pOwNy6Nukijc/WRCdXMSxBuO1zpGYwHzInqQzimbY8W1pG50Z4M8u2JLM1GsRp+H2+AStcgk+PQ==;EndpointSuffix=core.windows.net";
              
        public CloudTable ObterTabela(string nomeTabela)
        {
            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse(storegeConnectionString);

            CloudTableClient tableClient =  storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(nomeTabela);
            table.CreateIfNotExistsAsync().Wait();

            return table;
        }

        

    }
}
