using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace Modelo.Infra.Data.Interface
{
    public interface IAzureRepository
    {
        public CloudTable ObterTabela(string nomeTabela);


    }
}
