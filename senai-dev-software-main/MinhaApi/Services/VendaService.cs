
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

    public Venda? GetById(int id)
    {
        return _repo.GetById(id);
    }

    public Venda Update(Venda venda)
    {
        _repo.Update(venda);
        return venda;
    }

    
}

