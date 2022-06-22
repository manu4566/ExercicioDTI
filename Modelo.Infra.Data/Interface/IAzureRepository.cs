using Microsoft.Azure.Cosmos.Table;
using System;


namespace Modelo.Infra.Data.Interface
{
    public interface IAzureRepository
    {
        public CloudTable CriarTabela(string nomeTabela);


    }
}
