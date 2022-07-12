using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface IRealizarVendaService
    {
        Task CadastrarVenda(Venda venda);
        Task<DetalhesVenda> ObterDetalhesDaVenda(Guid id);       
        Task<List<Venda>> ObterTodasVendas(string cpf);

    }
}
