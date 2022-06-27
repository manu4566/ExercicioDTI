using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using System;


namespace Modelo.Infra.Data.Interface
{
    public interface IVendasRepository
    {
        public Task<Venda> ObterVenda(string id);
        public bool InserirVenda(Venda venda);        
        public Task<List<Venda>> ObterTodasVendas(string cpf);
    }
}
