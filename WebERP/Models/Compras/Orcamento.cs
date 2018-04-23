using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Estoque;

namespace WebERP.Models.Compras
{
    public class Orcamento : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SolicitacaoId { get; set; }

        [Required]
        public int FornecedorId { get; set; }

        [Required]
        public float PrecoUnitario { get; set; }

        [Required]
        public StatusOrcamento Status { get; set; }

        public Fornecedor Fornecedor { get; set; }
        public Solicitacao Solicitacao { get; set; }

        public float PrecoTotal()
        {
            return (Solicitacao?.QuantidadeSolicitada * PrecoUnitario) ?? 0;
        } 
    }

    public enum StatusOrcamento
    {
        EmAnalise,
        Negado,
        Aprovado,
    }
}
