using Modelo.Infra.Data.Entities;
using System;


namespace Modelo.Infra.Data.Interface
{
    public interface IVendasRepository
    {
        public VendaEntity ObterVenda(string id);
        public void InserirVenda(VendaEntity produto);        
        public List<VendaEntity> ObterTodasVendas(string cpf);
    }
}
