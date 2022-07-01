using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using Modelo.Domain.Interfaces;
using System;
using System.Text.Json;

namespace Modelo.Infra.Data.Repository
{
    public class VendasRepository : IVendasRepository
    {
        private readonly IBaseRepository _baseRepository;
        public VendasRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> InserirVenda(Venda venda)
        {
            var vendaEntity = ConverterVendaParaVendaEntity(venda);

            var result = await _baseRepository.InserirEntidade(vendaEntity, typeof(VendaEntity).Name);

            if (result.Result != null) return true;

            return false;
        }
     
        public async Task<List<Venda>> ObterTodasVendas(string cpf)
        {
            var vendaEntities = await _baseRepository.BuscarTodasEntidadesPartitionKeyAsync<VendaEntity>(cpf, typeof(VendaEntity).Name);

            return  ConverterVendasEntitiesParaVendas(vendaEntities);

        }

        public async Task<Venda> ObterVenda(string id)
        {
            var vendaEntities = await _baseRepository.BuscarTodasEntidadesRowKeyAsync<VendaEntity>(id, typeof(VendaEntity).Name);
            //Como o id da venda é unico, apesar de retornar uma lista, ela é de tamanho unitario ou nula, se não existir a venda com esse id

            return ConverterVendaEntityParaVenda(vendaEntities.First<VendaEntity>());
        }

        private VendaEntity ConverterVendaParaVendaEntity(Venda venda)
        {
            venda.Id = Guid.NewGuid();

            return new VendaEntity
            {
                PartitionKey = venda.Cpf,
                RowKey = venda.Id.ToString(),

                Id = venda.Id.ToString(),
                CPF = venda.Cpf,
                ProdutoVendidosJson = JsonSerializer.Serialize(venda.ProdutosVendidos)
            };
        }

        private Venda ConverterVendaEntityParaVenda(VendaEntity vendaEntity)
        {
            return new Venda
            {
                Id = Guid.Parse(vendaEntity.Id),
                Cpf = vendaEntity.CPF,
                ProdutosVendidos = JsonSerializer.Deserialize<List<ProdutoVendido>>(vendaEntity.ProdutoVendidosJson)
            };
        }

        private List<Venda> ConverterVendasEntitiesParaVendas(List<VendaEntity> vendasEntities)
        {
            var vendas = new List<Venda>();

            foreach (var item in vendasEntities)
            {
                vendas.Add(ConverterVendaEntityParaVenda(item));
            }

            return vendas;
        }

    }
}
