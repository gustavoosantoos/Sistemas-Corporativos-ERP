using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WebERP.Models.Dto
{
    public class NovaSolicitacaoDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Código do produto inválido.")]
        public int ProductId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "A quantidade solicitada deve ser maior do que 1.")]
        public float Quantidade { get; set; }
    }
}
