using Modelo.Application.DTO;
using Modelo.Domain.Models;


namespace Modelo.Application.Interfaces
{
    public interface IConverterVenda
    {
        public Venda VendaDto_Venda(VendaDto vendaDto);
        public List<VendaDto> Venda_VendaDto(List<Venda> vendas);
        public DetalhesVendaDto DetalhesVenda_DetalhesVendaDto(DetalhesVenda detalhesDaVenda);
    }
}
