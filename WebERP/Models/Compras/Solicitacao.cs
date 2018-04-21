using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebERP.Models.Estoque;
using WebERP.Models.Exceptions;

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

        public virtual List<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();
        public virtual Produto Produto { get; set; }
        public StatusSolicitacao Status { get; private set; }
        public virtual ApplicationUser Solicitante { get; set; }

        public void NegarSolicitacao()
        {
            ValidarMudancaDeStatus(StatusSolicitacao.Pendente);
            Status = StatusSolicitacao.Negado;
        }
        
        public void AprovarSolicitacaoParaOrcamentacao()
        {
            ValidarMudancaDeStatus(StatusSolicitacao.Pendente);
            Status = StatusSolicitacao.Orcamentacao;
        }

        public void AprovarSolicitacaoFinalizada()
        {
            ValidarMudancaDeStatus(StatusSolicitacao.Orcamentacao);
            Status = StatusSolicitacao.Aprovado;
        }

        public bool IsSolicitacaoFinalizada() =>
            Status == StatusSolicitacao.Aprovado || Status == StatusSolicitacao.Negado;

        public bool IsDisponivelParaRealizarPedido()
        {
            return Status == StatusSolicitacao.Orcamentacao && Orcamentos.Count > 0;
        }


        private void ValidarMudancaDeStatus(StatusSolicitacao statusToCheck)
        {
            if (Status != statusToCheck)
                throw new MudancaInvalidaDeStatusException(
                    "O status da solicitação é invalido para a operação solicitada.");
        }
    }
}
