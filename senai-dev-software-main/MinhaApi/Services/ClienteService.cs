using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;

    public ClienteService(IClienteRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Cliente> GetAll()
    {
        return _repo.GetAll();
    }

    public Cliente? GetById(int id)
    {
        return _repo.GetById(id);
    }

    public Cliente Create(Cliente cliente)
    {
        _repo.Add(cliente);
        return cliente;
    }

    public Cliente? Update(int id, Cliente cliente)
    {
        var existingCliente = _repo.GetById(id);
        if (existingCliente == null) return null;

        _repo.Update(cliente);
        return cliente;
    }

    public bool Delete(int id)
    {
        var existingCliente = _repo.GetById(id);
        if (existingCliente == null) return false;

        _repo.Delete(id);
        return true;
    }
}