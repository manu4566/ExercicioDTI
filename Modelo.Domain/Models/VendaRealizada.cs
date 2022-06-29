using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Models
{
    public class VendaRealizada
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public List<ProdutoVendido> ProdutoVendidos { get; set; }
    }
}
