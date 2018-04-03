namespace WebERP.Models.Estoque
{
    public class TelefoneFornecedor
    {
        public int Id { get; set; }
        public byte Ddd { get; set; }
        public string Numero { get; set; }

        public override string ToString()
        {
            return $"({Ddd}) {Numero}";
        }
    }
}