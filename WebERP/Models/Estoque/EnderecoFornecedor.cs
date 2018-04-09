using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebERP.Models.Estoque
{
    public class EnderecoFornecedor : IEntity
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Logradouro { get; set; }

        [StringLength(100)]
        public string Bairro { get; set; }

        [StringLength(100)]
        public string Cidade { get; set; }

        [StringLength(2)]
        public string Estado { get; set; }

        [StringLength(10), RegularExpression(@"\d{5}-\d{3}")]
        public string Cep { get; set; }
    }
}
