using Microsoft.Azure.Cosmos.Table;
using Modelo.Infra.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Infra.Data
{
    public class BaseRepository : IBaseRepository
    {

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TableEntity obj, string nomeTabela)
        {
            throw new NotImplementedException();
        }

        public IList<TableEntity> Select()
        {
            throw new NotImplementedException();
        }

        public TableEntity Select(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TableEntity obj)
        {
            throw new NotImplementedException();
        }

        private void ttt()
        {

        }

        public void ttt4444() { }
    }
}
