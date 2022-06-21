using Microsoft.Azure.Cosmos.Table;
using System;


namespace Modelo.Infra.Data.Interface
{
    public  interface IBaseRepository
    {
        void Insert(TableEntity obj, string nomeTabela);

        void Update(TableEntity obj);

        void Delete(int id);

        IList<TableEntity> Select();

        TableEntity Select(int id);
    }
}
