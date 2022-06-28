using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace Modelo.Infra.Data.Interface
{
    public  interface IBaseRepository
    {
        Task<TableResult> InserirEntidade(TableEntity obj, string nomeTabela);

        Task<TableResult> AtualizarEntidade(TableEntity obj, string nomeTabela);

        Task<TableResult> DeletarEntidade(TableEntity obj, string nomeTabela);

        Task<TEntity> BuscarEntidade<TEntity>(string partitionKey, string rowKey, string nomeTabela) where TEntity : TableEntity;       

        Task<List<TEntity>> BuscarTodasEntidadesAsync<TEntity>(string nomeTabela) where TEntity : TableEntity, new();

        Task<List<TEntity>> BuscarTodasEntidadesPartitionKeyAsync<TEntity>(string partitionKey, string nomeTabela) where TEntity : TableEntity, new();

        Task<List<TEntity>> BuscarTodasEntidadesRowKeyAsync<TEntity>(string rowKey, string nomeTabela) where TEntity : TableEntity, new();

        Task<List<TEntity>> BuscarEntidadesQueryAsync<TEntity>(TableQuery<TEntity> rangeQuery, string nomeTabela) where TEntity : TableEntity, new();

    }
}
