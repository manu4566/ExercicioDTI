using Microsoft.Azure.Cosmos.Table;
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
        public void Delete(int id, string nomeTabela)
        {
            throw new NotImplementedException();
        }

        public void Insert(TableEntity obj, string nomeTabela)
        {
            _azureRepository.CriarTabela(nomeTabela);

        }

        public IList<TableEntity> Select(string nomeTabela)
        {
            throw new NotImplementedException();
        }

        public TableEntity Select(string id, string nomeTabela)
        {
            throw new NotImplementedException();
        }

        public void Update(TableEntity obj, string nomeTabela)
        {
            throw new NotImplementedException();
        }

       
    }
}
