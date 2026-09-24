
namespace MinhaApi.Models;

public class Fornecedores
{
    
    public int Id {get; set; }
    
    public string Nome {get; set; } = string.Empty;
    public string email {get; set;} = string.Empty;
    public string Cnpj {get; set; } = string.Empty;
    
    public DateTime Data_venda {get; set;} 

    public int Cep {get; set;} 

    public int numero {get; set;} 

    public string Complemento {get; set;} = string.Empty;
    public string Celular {get; set;} = string.Empty;

    public string telefone {get; set;} = string.Empty;
    
    public bool Ativo {get; set;}
    
    
}