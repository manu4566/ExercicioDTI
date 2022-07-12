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

            return await table.ExecuteAsync(insert);

        }

        public async Task<TableResult> AtualizarEntidade(TableEntity obj, string nomeTabela) //ok
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableOperation merge = TableOperation.InsertOrMerge(obj);

            // Executar a operacao
            return await table.ExecuteAsync(merge);
        }
         

        public async Task<TableResult> DeletarEntidade(TableEntity obj, string nomeTabela) //ok
        {
            TableOperation deleteOperation = TableOperation.Delete(obj);
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);

            return await table.ExecuteAsync(deleteOperation);
        }

        public async Task<TEntity> BuscarEntidade<TEntity>(string partitionKey, string rowKey, string nomeTabela) where TEntity : TableEntity //ok
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableOperation retrive = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);

            var result = await table.ExecuteAsync(retrive);

            return (TEntity)result.Result;

        }
       
        public async Task<List<TEntity>> BuscarTodasEntidadesPartitionKeyAsync<TEntity>(string partitionKey, string nomeTabela) 
            where TEntity : TableEntity, new()
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableQuery<TEntity> rangeQuery = new TableQuery<TEntity>().Where(
                     TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            TableContinuationToken token = new TableContinuationToken();

            var tableEntities = new List<TEntity>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);
                tableEntities.AddRange(queryResult);

                //Atualiza o token
                token = queryResult.ContinuationToken;

            } while (token != null);

            return tableEntities;

        }

        public async Task<List<TEntity>> BuscarTodasEntidadesRowKeyAsync<TEntity>(string rowKey, string nomeTabela)
            where TEntity : TableEntity, new()
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            TableQuery<TEntity> rangeQuery = new TableQuery<TEntity>().Where(
                     TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));

            TableContinuationToken token = new TableContinuationToken();

            var tableEntities = new List<TEntity>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);
                tableEntities.AddRange(queryResult);

                //Atualiza o token
                token = queryResult.ContinuationToken;

            } while (token != null);

            return tableEntities;

        }

        public async Task<List<TEntity>> BuscarEntidadesQueryAsync<TEntity>(TableQuery<TEntity> rangeQuery,string nomeTabela) where TEntity : TableEntity, new()
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);
            //Inicialize o token de continuação para null para iniciar do início da tabela.
            TableContinuationToken token = new TableContinuationToken();

            var tableEntities = new List<TEntity>();

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

        public async Task<List<TEntity>> BuscarTodasEntidadesAsync<TEntity>(string nomeTabela) where TEntity : TableEntity, new()
        {
            CloudTable table = _azureRepository.ObterTabela(nomeTabela);            
            //Inicialize o token de continuação para null para iniciar do início da tabela.
            TableContinuationToken token = new TableContinuationToken();

            var tableEntities = new List<TEntity>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<TEntity>(), token);
                tableEntities.AddRange(queryResult);

                //Atualiza o token
                token = queryResult.ContinuationToken;


                //Faz um loop até que um token de continuação nulo seja recebido, indicando o final da tabela.
            } while (token != null);

            return tableEntities;
        }
    }
}
