using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;

public class DepartamentoService : IDepartamentoService
{
    private readonly IDepartamentoRepository _repo;

    public DepartamentoService(IDepartamentoRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Departamento> GetAll()
    {
        return _repo.GetAll();
    }

    public Departamento? GetById(int id)
    {
        return _repo.GetById(id);
    }

    public Departamento Add(Departamento departamento)
    {
        _repo.Add(departamento);
        return departamento;
    }

    public Departamento? Update(int id, Departamento departamento)
    {
        var existingDepartamento = _repo.GetById(id);
        if (existingDepartamento == null) return null;

        _repo.Update(departamento);
        return departamento;
    }

    public bool Delete(int id)
    {
        var existingDepartamento = _repo.GetById(id);
        if (existingDepartamento == null) return false;

        _repo.Delete(id);
        return true;
    }
}