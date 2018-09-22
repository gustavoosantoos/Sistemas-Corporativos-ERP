using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Estoque;

namespace WebERP.Models.Vendas
{
    public class ProdutoVenda : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }
        
        public Produto Produto { get; set; }

        [Required]
        public double Quantidade { get; set; }

        [Required]
        public double PrecoUnitario { get; set; }

        [NotMapped]
        public double PrecoTotal => Quantidade * PrecoUnitario; 
    }
}
