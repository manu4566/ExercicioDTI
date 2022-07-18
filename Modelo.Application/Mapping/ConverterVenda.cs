using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Models;

namespace Modelo.Application.Mapping
{
    public  class ConverterVenda : IConverterVenda
    {
        private readonly IConverterProduto _converterProduto;
        public ConverterVenda(IConverterProduto converterProduto)
        {
            _converterProduto = converterProduto;
        }

        public  Venda VendaDtoParaVenda(VendaDto vendaDto)
        {
            return new Venda
            {
                Id = new Guid(),
                ProdutosVendidos = ProdutosVendidosDto_ProdutosVendidos(vendaDto.ProdutosVendidos),
                Cpf = vendaDto.Cpf

            };
        }

        public DetalhesVendaDto DetalhesVendaParaDetalhesVendaDto(DetalhesVenda detalhesDaVenda)
        {
            return new DetalhesVendaDto
            {
                Id = detalhesDaVenda.Id,
                Cpf = detalhesDaVenda.Cpf,
                ValorTotal = detalhesDaVenda.ValorTotal,
                ProdutoVendidosDto = _converterProduto.ProdutosParaProdutosDto(detalhesDaVenda.ProdutosVendidos)

            };
        }

        public  List<VendaDto> VendasParaVendasDto(List<Venda> vendas)
        {
            var vendasDto = new List<VendaDto>();
           
            foreach (var venda in vendas)
            {
                vendasDto.Add(VendaParaVendaDto(venda));
            }

            return vendasDto;
        }
       
        private  VendaDto VendaParaVendaDto(Venda venda)
        {
            return new VendaDto
            {
                Id = venda.Id.ToString(),
                ProdutosVendidos = ProdutosVendidos_ProdutosVendidosDto(venda.ProdutosVendidos),
                Cpf = venda.Cpf

            };
        }
        private  List<ProdutoVendidoDto> ProdutosVendidos_ProdutosVendidosDto(List<ProdutoVendido> produtosVendidos)
        {
            var produtosVendidosDto = new List<ProdutoVendidoDto>();

            foreach (var produtoVendido in produtosVendidos)
            {
                produtosVendidosDto.Add(ProdutoVendido_ProdutoVendidoDto(produtoVendido));
            }
            return produtosVendidosDto;
        }

        private  ProdutoVendidoDto ProdutoVendido_ProdutoVendidoDto(ProdutoVendido produtoVendido)
        {
            return new ProdutoVendidoDto
            {
                Id = produtoVendido.Id,
                QtdVendida = produtoVendido.QtdVendida

            };
        }      

        private List<ProdutoVendido> ProdutosVendidosDto_ProdutosVendidos(List<ProdutoVendidoDto> produtosVendidosDto)
        {
            var produtosVendidos = new List<ProdutoVendido>();

            foreach (var produtoVendidoDto in produtosVendidosDto)
            {
                produtosVendidos.Add(ProdutoVendidoDto_ProdutoVendido(produtoVendidoDto));
            }
            return produtosVendidos;
        }

        private ProdutoVendido ProdutoVendidoDto_ProdutoVendido(ProdutoVendidoDto produtoVendidoDto)
        {
            return new ProdutoVendido
            {
                Id = produtoVendidoDto.Id,
                QtdVendida = produtoVendidoDto.QtdVendida

            };
        }

    }
}
