using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebERP.Models.Estoque;

namespace WebERP.Data.Repositories
{
    public class FornecedoresRepository : BaseRepository<Fornecedor>
    {
        public FornecedoresRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Fornecedor> ListAll()
        {
            return All()
                .Include(e => e.Telefone)
                .Include(e => e.Endereco)
                .ToList();
        }

        public override Fornecedor FindById(int id)
        {
            return All()
                .Include(e => e.Telefone)
                .Include(e => e.Endereco)
                .SingleOrDefault(e => e.Id == id);
        }

        public override void Save(Fornecedor entity)
        {
            _context.EnderecoFornecedores.Add(entity.Endereco);
            _context.TelefoneFornecedores.Add(entity.Telefone);
            base.Save(entity);
        }
    }
}
