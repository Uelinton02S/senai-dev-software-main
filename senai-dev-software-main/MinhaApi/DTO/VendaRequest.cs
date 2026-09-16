namespace MinhaApi.DTO
{
    public class VendaRequest
    {
        public int idcliente { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public List<ItemVendaRequest> Itens { get; set; } = new List<ItemVendaRequest>();
    }

    public class ItemVendaRequest
    {
        public int idproduto { get; set; }
        public int quantidade { get; set; }
        public decimal valorunitario { get; set; }
    }
}
