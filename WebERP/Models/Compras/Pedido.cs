using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models.Compras
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        public int OrcamentoId { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }
        
        public Orcamento Orcamento { get; set; }
    }
}
