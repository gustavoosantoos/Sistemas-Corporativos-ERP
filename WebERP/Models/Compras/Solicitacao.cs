using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Estoque;

namespace WebERP.Models.Compras
{
    public class Solicitacao : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int SolicitanteId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public float QuantidadeSolicitada { get; set; }

        public List<Orcamento> Orcamentos { get; set; }
        public Produto Produto { get; set; }
        public StatusSolicitacao Status { get; set; }
        public ApplicationUser Solicitante { get; set; }
    }

    public enum StatusSolicitacao
    {
        Pendente,
        Orcamentacao,
        Negado,
        Aprovado
    }
}
