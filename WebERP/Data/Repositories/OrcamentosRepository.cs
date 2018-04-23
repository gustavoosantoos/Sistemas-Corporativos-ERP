using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebERP.Models.Compras;

namespace WebERP.Data.Repositories
{
    public class OrcamentosRepository : BaseRepository<Orcamento>
    {
        public OrcamentosRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Orcamento FindById(int id)
        {
            return All()
                .Include(e => e.Fornecedor)
                .Include(e => e.Solicitacao)
                .SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<Orcamento> GetOrcamentosDaSolicitacao(int idValue)
        {
            return All()
                .Include(e => e.Fornecedor)
                .Include(e => e.Solicitacao)
                .Where(e => e.SolicitacaoId == idValue).ToList();
        }
    }
}
