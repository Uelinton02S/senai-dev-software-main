
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
    
    public IEnumerable<Venda> GetAll()
    {
        return _repo.GetAll();
    }

    public void Add(Venda venda)
    {
        _repo.Add(venda);
    }

    public Venda Update(Venda venda)
    {
        _repo.Update(venda);
        return venda;
    }

    
}

