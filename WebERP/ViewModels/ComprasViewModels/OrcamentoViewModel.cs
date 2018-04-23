using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Compras;
using WebERP.Models.Estoque;

namespace WebERP.ViewModels.ComprasViewModels
{
    public class OrcamentoViewModel
    {
        public Produto Produto{ get; set; }
        public Solicitacao Solicitacao { get; set; }
        public string Solicitante { get; set; }

        public IEnumerable<Orcamento> Orcamentos { get; set; }
        public IEnumerable<Fornecedor> FornecedoresDisponiveis { get; set; }
    }
}
