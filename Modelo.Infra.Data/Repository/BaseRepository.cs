using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Infra.Data.Interface;
using System;


namespace Modelo.Infra.Data.Repository
{
    public class BaseRepository : IBaseRepository
    {

        private readonly IAzureRepository _azureRepository;
        public BaseRepository(IAzureRepository azureRepository)
        {
            _azureRepository = azureRepository;
        }
        
        public async Task<TableResult> InserirEntidade(TableEntity obj, string nomeTabela) //ok
        {
           CloudTable table = _azureRepository.ObterTabela(nomeTabela);
           TableOperation insert = TableOperation.Insert(obj);

            // Executar a operacao
            return  await table.ExecuteAsync(insert);                    
      
        }

        public async Task<TableResult> AtualizarEntidade(TableEntity obj, string nomeTabela) //ok
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableOperation merge = TableOperation.Merge(obj);

            // Executar a operacao
            return await table.ExecuteAsync(merge);
        }


        public async Task<TableResult> SelecionarEntidade(string partitionKey, string rowKey, string nomeTabela) //ok
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableOperation retrive = TableOperation.Retrieve<TableEntity>(partitionKey, rowKey);

            // Executar a operacao
            return await table.ExecuteAsync(retrive);
        }

        public async Task<TableResult> DeletarEntidade(TableEntity obj, string nomeTabela) //ok
        {
            TableOperation deleteOperation = TableOperation.Delete(obj);
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);

            return await table.ExecuteAsync(deleteOperation);
        }

      
        public async Task<List<TableEntity>> BuscarTodasEntidadesRowKeyAsync(string rowKey, string nomeTabela)  //ok
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableQuery<TableEntity> rangeQuery = new TableQuery<TableEntity>().Where(
                     TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));

            TableContinuationToken token = null;

            var tableEntities = new List<TableEntity>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);
                tableEntities.AddRange(queryResult);
              
                //Atualiza o token
                token = queryResult.ContinuationToken;

            } while (token != null);
           
            return tableEntities;           
          
        }

        public async Task<List<TableEntity>> BuscarTodasEntidadesPartitionKeyAsync(string partitionKey, string nomeTabela)  //ok
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableQuery<TableEntity> rangeQuery = new TableQuery<TableEntity>().Where(
                     TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            TableContinuationToken token = null;

            var tableEntities = new List<TableEntity>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);
                tableEntities.AddRange(queryResult);

                //Atualiza o token
                token = queryResult.ContinuationToken;

            } while (token != null);

            return tableEntities;

        }

        public async Task<List<TableEntity>> BuscarTodasEntidadesAsync(string nomeTabela)
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableQuery<TableEntity> rangeQuery = new TableQuery<TableEntity>();

            //Inicialize o token de continuação para null para iniciar do início da tabela.
            TableContinuationToken token = null;

            var tableEntities = new List<TableEntity>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);
                tableEntities.AddRange(queryResult);

                //Atualiza o token
                token = queryResult.ContinuationToken;


                //Faz um loop até que um token de continuação nulo seja recebido, indicando o final da tabela.
            } while (token != null);

            return tableEntities;
        }
    }
}
