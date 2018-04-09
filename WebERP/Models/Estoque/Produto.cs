using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models.Estoque
{
    public class Produto : IEntity
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Nome { get; set; }

        [Required, StringLength(255)]
        public string Descricao { get; set; }

        [Required]
        public string UnidadeDeMedida { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public int Minimo { get; set; }

        public bool IsCandidatoAPedidos()
        {
            return Quantidade <= Minimo;
        }
    }
}
