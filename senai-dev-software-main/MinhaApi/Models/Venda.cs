namespace MinhaApi.Models;

public class Venda {

     public int id { get; set; }
    public int Id { get; internal set; }
    public DateTime data_venda {get ; set; }   
    
    public int idclientes { get; set;}
    public int idprodutos {get; set;}
    public int quantidade { get; set; }


 
}