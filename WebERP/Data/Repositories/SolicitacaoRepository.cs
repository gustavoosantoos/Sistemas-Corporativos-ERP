using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebERP.Models.Compras;
using WebERP.Models.Estoque;

namespace WebERP.Data.Repositories
{
    public class SolicitacaoRepository : BaseRepository<Solicitacao>
    {
        public SolicitacaoRepository(ApplicationDbContext context) : base(context)
        {
        }


        /// <summary>
        /// Pega a última solicitação do product que esteja em aberto.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Solicitacao PegaSolicitacaoEmAbertoDoProduto(int productId)
        {
            var solicitacao = All().Include(e => e.Solicitante)
                .SingleOrDefault(e => e.ProdutoId == productId && !e.IsSolicitacaoFinalizada());
            return solicitacao;
        }

        public bool ProdutoPossuiSolicitacaoAberta(int productId)
        {
            var possuiSolicitacao = All().Any(e => e.ProdutoId == productId && !e.IsSolicitacaoFinalizada());
            return possuiSolicitacao;
        }
    }
}
