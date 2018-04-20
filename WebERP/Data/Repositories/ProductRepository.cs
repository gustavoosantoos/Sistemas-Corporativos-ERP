using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WebERP.Models.Estoque;

namespace WebERP.Data.Repositories
{
    public class ProductRepository : BaseRepository<Produto>
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public List<Produto> ListAll()
        {
            return base.All().ToList();
        }
    }
}
