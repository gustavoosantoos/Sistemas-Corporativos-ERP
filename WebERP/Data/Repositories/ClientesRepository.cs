using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebERP.Models.Clientes;

namespace WebERP.Data.Repositories
{
    public class ClientesRepository : BaseRepository<Cliente>
    {
        public ClientesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Cliente> ListAll() => All().ToList();
    }
}
