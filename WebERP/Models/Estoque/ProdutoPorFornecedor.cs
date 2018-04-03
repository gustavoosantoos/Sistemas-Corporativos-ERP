using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models.Estoque
{
    public class ProdutoPorFornecedor
    {
        public int FornecedorId { get; set; }
        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
