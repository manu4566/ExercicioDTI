using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace Modelo.Infra.Data.Interface
{
    public  interface IBaseRepository
    {
        Task<TableResult> InserirEntidade(TableEntity obj, string nomeTabela);

        Task<TableResult> AtualizarEntidade(TableEntity obj, string nomeTabela);

        Task<TableResult> DeletarEntidade(TableEntity obj, string nomeTabela);

        Task<TableResult> SelecionarEntidade(string partitionKey, string rowKey, string nomeTabela);

        Task<List<TableEntity>> BuscarTodasEntidadesRowKeyAsync(string rowKey, string nomeTabela);       

        Task<List<TableEntity>> BuscarTodasEntidadesPartitionKeyAsync(string partitionKey, string nomeTabela);

        Task<List<TableEntity>> BuscarTodasEntidadesAsync(string nomeTabela);
    }
}
