using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models.Estoque
{
    public class Produto : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Nome { get; set; }

        [Required, StringLength(255)]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Unidade de Medida")]
        public string UnidadeDeMedida { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Quantidade { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Minimo { get; set; }

        public bool IsCandidatoAPedidos()
        {
            return Quantidade <= Minimo;
        }
    }
}
