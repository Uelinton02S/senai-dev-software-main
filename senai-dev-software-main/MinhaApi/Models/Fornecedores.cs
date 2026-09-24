using System.Runtime.CompilerServices;

namespace MinhaApi.Models;

public class Fornecedores
{
    
    public int Id {get; set; }
    
    public string Nome {get; set; }
    public string email {get; set;}
    public string Cnpj {get; set; }
    
    public DateTime Data_venda {get; set;}

    public int Cep {get; set;}

    public int numero {get; set;}

    public string Complemento {get; set; }
    public string Celular {get; set;}

    public string telefone {get; set;}
    
    public bool Ativo {get; set;}
    
    
}