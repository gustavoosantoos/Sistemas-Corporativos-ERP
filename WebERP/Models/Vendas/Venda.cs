using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Clientes;
using WebERP.Models.Estoque;

namespace WebERP.Models.Vendas
{
    public class Venda : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public List<ProdutoVenda> Produtos { get; set; }

        public DateTime DataHoraVenda { get; set; } 

        [NotMapped]
        public double ValorTotal => Produtos?.Sum(p => p.PrecoTotal) ?? 0;
    }
}
