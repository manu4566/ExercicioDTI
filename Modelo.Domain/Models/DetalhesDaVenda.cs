using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Models
{
    public class DetalhesDaVenda
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public List<Produto> ProdutoVendidos { get; set; }
        public float ValorTotal { get; set; }
    }
}
