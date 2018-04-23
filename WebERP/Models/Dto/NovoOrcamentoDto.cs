using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models.Dto
{
    public class NovoOrcamentoDto
    {
        public int SolicitacaoId { get; set; }
        public int FornecedorId { get; set; }
        public float PrecoUnitario { get; set; }
    }
}
