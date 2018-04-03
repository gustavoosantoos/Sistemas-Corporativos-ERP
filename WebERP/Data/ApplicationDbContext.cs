using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebERP.Models;
using WebERP.Models.Compras;
using WebERP.Models.Estoque;

namespace WebERP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<EnderecoFornecedor> EnderecoFornecedores { get; set; }
        public DbSet<TelefoneFornecedor> TelefoneFornecedores { get; set; }
        public DbSet<ProdutoPorFornecedor> ProdutorPorFornecedores { get; set; }

        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProdutoPorFornecedor>().HasKey(e => new {e.ProdutoId, e.FornecedorId});

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
