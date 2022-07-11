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

        public  Venda VendaDto_Venda(VendaDto vendaDto)
        {
            return new Venda
            {
                Id = new Guid(),
                ProdutosVendidos = ProdutosVendidosDto_ProdutosVendidos(vendaDto.ProdutosVendidos),
                Cpf = vendaDto.Cpf

            };
        }
        public  List<ProdutoVendido> ProdutosVendidosDto_ProdutosVendidos(List<ProdutoVendidoDto> produtosVendidosDto)
        {
            var produtosVendidos = new List<ProdutoVendido>();

            foreach (var produtoVendidoDto in produtosVendidosDto)
            {
                produtosVendidos.Add(ProdutoVendidoDto_ProdutoVendido(produtoVendidoDto));
            }
            return produtosVendidos;
        }       
        public  ProdutoVendido ProdutoVendidoDto_ProdutoVendido(ProdutoVendidoDto produtoVendidoDto)
        {
            return new ProdutoVendido
            {
                Id = produtoVendidoDto.Id,
                QtdVendida = produtoVendidoDto.QtdVendida

            };
        }
        
        public  List<VendaDto> Venda_VendaDto(List<Venda> vendas)
        {
            var vendasDto = new List<VendaDto>();
           
            foreach (var venda in vendas)
            {
                vendasDto.Add(Venda_VendaDto(venda));
            }

            return vendasDto;
        }
        public  VendaDto Venda_VendaDto(Venda venda)
        {
            return new VendaDto
            {                
                ProdutosVendidos = ProdutosVendidos_ProdutosVendidosDto(venda.ProdutosVendidos),
                Cpf = venda.Cpf

            };
        }
        public  List<ProdutoVendidoDto> ProdutosVendidos_ProdutosVendidosDto(List<ProdutoVendido> produtosVendidos)
        {
            var produtosVendidosDto = new List<ProdutoVendidoDto>();

            foreach (var produtoVendido in produtosVendidos)
            {
                produtosVendidosDto.Add(ProdutoVendido_ProdutoVendidoDto(produtoVendido));
            }
            return produtosVendidosDto;
        }
        public  ProdutoVendidoDto ProdutoVendido_ProdutoVendidoDto(ProdutoVendido produtoVendido)
        {
            return new ProdutoVendidoDto
            {
                Id = produtoVendido.Id,
                QtdVendida = produtoVendido.QtdVendida

            };
        }

        public  DetalhesVendaDto DetalhesVenda_DetalhesVendaDto(DetalhesVenda detalhesDaVenda)
        {
            return new DetalhesVendaDto
            {
                Id = detalhesDaVenda.Id,
                Cpf = detalhesDaVenda.Cpf,
                ValorTotal = detalhesDaVenda.ValorTotal,
                ProdutoVendidosDto = _converterProduto.Produtos_ProdutosDto(detalhesDaVenda.ProdutosVendidos)

            };
        }
       

    }
}
