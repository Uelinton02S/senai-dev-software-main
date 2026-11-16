using MinhaApi.Models;

namespace MinhaApi.Repositories;

public interface IFornecedoresRepository
{
  IEnumerable<Fornecedores> GetAll();
    Fornecedores? GetById(int id);
    void Create(Fornecedores fornecedores);
    void Update(Fornecedores fornecedores);
    void Delete(int id);
}