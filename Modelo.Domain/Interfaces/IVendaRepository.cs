using Modelo.Domain.Models;
using System;

namespace Modelo.Domain.Interfaces
{
    public interface IVendaRepository
    {
        public Task<Venda> ObterVenda(string id);
        public Task<bool> InserirVenda(Venda venda);        
        public Task<List<Venda>> ObterTodasVendas(string cpf);
    }
}
