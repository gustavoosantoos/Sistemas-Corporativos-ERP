using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebERP.Models.Estoque;

namespace WebERP.Models.Compras
{
    public class Solicitacao : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(Solicitante))]
        public string SolicitanteId { get; set; }

        [Required, ForeignKey(nameof(Produto))]
        public int ProdutoId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public float QuantidadeSolicitada { get; set; }

        public List<Orcamento> Orcamentos { get; set; }
        public Produto Produto { get; set; }
        public StatusSolicitacao Status { get; set; }
        public ApplicationUser Solicitante { get; set; }

        public bool IsSolicitacaoFinalizada() =>
            Status == StatusSolicitacao.Aprovado || Status == StatusSolicitacao.Negado;
    }
    
    public enum StatusSolicitacao
    {
        Pendente,
        Orcamentacao,
        Negado,
        Aprovado
    }
}
