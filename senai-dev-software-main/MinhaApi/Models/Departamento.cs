namespace MinhaApi.Models;


public class Departamento 
{
    
    public int Id {get; set; }
    public String Nome {get ; set; }  = string.Empty;
    public String email {get; set; } = string.Empty;

    public int idfornecedores {get; set;}
    public int iddepartamento {get; set;}
    public String telefone {get; set; } = string.Empty;
    public String descricao {get; set; } = string.Empty;
    public bool Ativo {get; set; }

}