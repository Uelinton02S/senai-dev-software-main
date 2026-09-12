
using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

namespace MinhaApi.Repositories;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _repo;


    public VendaService(IVendaRepository repo)
    {
        _repo = repo;
    }
    
    public void GetclienteById(int id)
    {
        if (id == 0){
            throw new ArgumentException("Id inválido");
        }
    }

    public void GetProdutoById(int id)
    {
        if (id == 0){
            throw new ArgumentException("Id inválido");
        }
    }
    
    public Venda Update(Venda venda)
    {
        _repo.Update(venda);
        return venda;
    }

    
}

