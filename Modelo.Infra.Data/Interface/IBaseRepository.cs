using Microsoft.Azure.Cosmos.Table;
using System;


namespace Modelo.Infra.Data.Interface
{
    public  interface IBaseRepository
    {
        void Insert(TableEntity obj, string nomeTabela);

        void Update(TableEntity obj, string nomeTabela);

        void Delete(int id, string nomeTabela);

        IList<TableEntity> Select(string nomeTabela);

        TableEntity Select(string id, string nomeTabela);
    }
}
