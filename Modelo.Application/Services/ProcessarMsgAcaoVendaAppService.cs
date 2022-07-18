using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Application.Mapping;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Share;
using System;

namespace Modelo.Application.Services
{
    public class ProcessarMsgAcaoVendaAppService : IProcessarMsgAcaoVendaAppService
    {
        private readonly IRealizarVendaService _realizarVendaService;
        
        private readonly IConverterVenda _converterVenda;

        public ProcessarMsgAcaoVendaAppService( 
            IRealizarVendaService realizarVendaService,
            IConverterVenda converterVenda)
        {
            _realizarVendaService = realizarVendaService;
            _converterVenda = converterVenda;
        }

        public Task<MensagemRetornoAcaoVenda> ProcessarMsgAcaoVenda(MensagemAcaoVenda msgAcaoVenda)
        {
            switch(msgAcaoVenda.Acao)
            {
                case Enum.AcaoVenda.CadastrarVenda:

                    return CadastrarVenda(msgAcaoVenda);
                    
                case Enum.AcaoVenda.ObterDetalhesVenda:

                    return ObterDetalhesVenda(msgAcaoVenda);

                case Enum.AcaoVenda.ObterTodasVendas:

                    return ObterTodasVendas(msgAcaoVenda);
                  
                default: 
                    return null;

            }   
        }

        private async Task<MensagemRetornoAcaoVenda> CadastrarVenda(MensagemAcaoVenda msgAcaoVenda)
        {
            var venda = _converterVenda.VendaDtoParaVenda(msgAcaoVenda.Venda);

            return new MensagemRetornoAcaoVenda
            {
                MensagemRetorno = await _realizarVendaService.CadastrarVenda(venda)
            };
        }

        private async Task<MensagemRetornoAcaoVenda> ObterDetalhesVenda(MensagemAcaoVenda msgAcaoVenda)
        {
            var retorno = new MensagemRetornoAcaoVenda();

            var detalhesDaVenda = await _realizarVendaService.ObterDetalhesDaVenda(msgAcaoVenda.Id);

            if(detalhesDaVenda != null)
            {
               retorno.DetalhesDaVendaDto = _converterVenda.DetalhesVendaParaDetalhesVendaDto(detalhesDaVenda);
               retorno.MensagemRetorno = AppConstantes.Api.Sucesso.Busca;
            }
            else
            {
               retorno.MensagemRetorno = AppConstantes.Api.Erros.NaoEncontrado;
            }

            return retorno;
            
                
            
        }

        private async Task<MensagemRetornoAcaoVenda> ObterTodasVendas(MensagemAcaoVenda msgAcaoVenda)
        {
            var retorno = new MensagemRetornoAcaoVenda();

            var venda = await _realizarVendaService.ObterTodasVendas(msgAcaoVenda.Cpf);

            if(venda != null)
            {
                retorno.VendasDto = _converterVenda.VendasParaVendasDto(venda);
                retorno.MensagemRetorno = AppConstantes.Api.Sucesso.Busca;
            }
            else
            {
                retorno.MensagemRetorno = AppConstantes.Api.Erros.NaoEncontrado;
            }

            return retorno;
        }

       
    }
}
