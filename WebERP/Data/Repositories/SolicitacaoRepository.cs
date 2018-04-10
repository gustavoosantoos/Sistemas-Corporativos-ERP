using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Compras;
using WebERP.Models.Estoque;

namespace WebERP.Data.Repositories
{
    public class SolicitacaoRepository : BaseRepository<Solicitacao>
    {
        public SolicitacaoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool ProdutoPossuiSolicitacaoAberta(int productId)
        {
            var possuiSolicitacao = All().Any(e => e.ProdutoId == productId && !e.IsSolicitacaoFinalizada());
            return possuiSolicitacao;
        }
    }
}
