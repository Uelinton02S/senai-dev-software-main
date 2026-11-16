using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;

public class FornecedoresService : IFornecedoresService
{
    private readonly IFornecedoresRepository _repo;

    public FornecedoresService(IFornecedoresRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Fornecedores> GetAll()
    {
        return _repo.GetAll();
    }

    public Fornecedores? GetById(int id)
    {
        return _repo.GetById(id);
    }

    public Fornecedores Create(Fornecedores fornecedores)
    {
        _repo.Create(fornecedores);
        return fornecedores;
    }

    public Fornecedores? Update(int id, Fornecedores fornecedores)
    {
        var existingFornecedores = _repo.GetById(id);
        if (existingFornecedores == null) return null;

        _repo.Update(fornecedores);
        return fornecedores;
    }

    public bool Delete(int id)
    {
        var existingFornecedor = _repo.GetById(id);
        if (existingFornecedor == null) return false;

        _repo.Delete(id);
        return true;
    }
}