using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace WebERP.Models.Estoque
{
    public class TelefoneFornecedor : IEntity
    {
        [Key]
        public int Id { get; set; }
        public byte Ddd { get; set; }
        public string Numero { get; set; }

        public override string ToString()
        {
            return $"({Ddd}) {Numero}";
        }
    }
}