using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models.Estoque
{
    public class Fornecedor : IEntity
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string NomeFantasia { get; set; }

        [Required, StringLength(255)]
        public string RazaoSocial  { get; set; }

        [StringLength(20)]
        public string Cnpj { get; set; }

        [StringLength(60), EmailAddress]
        public string Email { get; set; }

        public EnderecoFornecedor Endereco { get; set; }
        public List<TelefoneFornecedor> Telefone { get; set; }
    }
}
