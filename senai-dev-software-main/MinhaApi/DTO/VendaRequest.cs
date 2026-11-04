namespace MinhaApi.DTO
{
    public class VendaRequest
    {
        public int idprodutos { get; set; } // Verifique se não está "idprodutos" ou "produtoId"
        public int idclientes { get; set; }
        public int quantidade { get; set; }
    }
}
