using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;


public interface IFornecedoresService
{
    IEnumerable<Fornecedores> GetAll();
   
    Fornecedores? GetById(int id);
   
    Fornecedores Create(Fornecedores fornecedores);

    Fornecedores? Update(int id ,Fornecedores fornecedores);

    bool Delete(int id);
   
}