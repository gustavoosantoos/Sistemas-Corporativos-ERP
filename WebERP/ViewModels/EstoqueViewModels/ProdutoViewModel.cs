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
            return Solicitacao != null;
        }
    }
}
