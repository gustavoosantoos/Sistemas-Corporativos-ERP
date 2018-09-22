using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Vendas;

namespace WebERP.Data.Repositories
{
    public class VendasRepository : BaseRepository<Venda>
    {
        public VendasRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Venda> ListAll()
        {
            return All()
                .Include(v => v.Cliente)
                .Include(v => v.Produtos)
                .ToList();
        }
    }
}
