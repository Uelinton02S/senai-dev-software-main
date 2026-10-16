namespace MinhaApi.DTO
{
    public class VendaRequest
    {
        public int idproduto { get; set; } // Verifique se não está "idprodutos" ou "produtoId"
        public int idcliente { get; set; }
        public int quantidade { get; set; }
    }
}
