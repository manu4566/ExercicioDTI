using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using System;

namespace Modelo.Infra.Data.Repository
{
    public class VendasRepository : IVendasRepository
    {
        private readonly IBaseRepository _baseRepository;
        public VendasRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public bool InserirVenda(Venda venda)
        {
            var vendaEntity = ConverterVendaToVendaEntity(venda);

            var result = _baseRepository.InserirEntidade(vendaEntity, typeof(VendaEntity).Name);

            if (result.Result != null) return true;

            return false;
        }
     
        public Task<List<Venda>> ObterTodasVendas(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<Venda> ObterVenda(string id)
        {
            throw new NotImplementedException();
        }

        private VendaEntity ConverterVendaToVendaEntity(Venda venda)
        {
            return new VendaEntity 
            { 
            };
        }

      

    }
}
