using Modelo.Application.DTO;
using Modelo.Domain.Models;


namespace Modelo.Application.Interfaces
{
    public interface IConverterVenda
    {
        public Venda VendaDtoParaVenda(VendaDto vendaDto);
        public List<VendaDto> VendasParaVendasDto(List<Venda> vendas);
        public DetalhesVendaDto DetalhesVendaParaDetalhesVendaDto(DetalhesVenda detalhesDaVenda);
    }
}
