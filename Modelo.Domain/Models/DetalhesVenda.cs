using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Models
{
    public class DetalhesVenda
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public List<Produto> ProdutosVendidos { get; set; }
        public double ValorTotal { get; set; }
    }
}
