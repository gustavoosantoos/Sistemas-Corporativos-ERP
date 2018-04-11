using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Compras;
using WebERP.Models.Estoque;

namespace WebERP.ViewModels.EstoqueViewModels
{
    public class ProdutoViewModel
    {
        public Produto Produto { get; set; }
        public Solicitacao Solicitacao { get; set; }

        public bool PossuiSolicitacaoAtiva()
        {
            return Solicitacao != null && !Solicitacao.IsSolicitacaoFinalizada();
        }

        public string CorParaStatus()
        {
            if (Solicitacao == null)
                return "";

            StatusSolicitacao s = Solicitacao.Status;

            switch (s)
            {
                case StatusSolicitacao.Aprovado:
                    return "color: green;";
                case StatusSolicitacao.Negado:
                    return "color: red;";
                case StatusSolicitacao.Orcamentacao:
                    return "color: blue;";
                default:
                    return "";
            }
        }
    }
}
