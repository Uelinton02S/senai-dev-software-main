using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;


public interface IDepartamentoService
{
    IEnumerable<Departamento> GetAll();
   
    Departamento? GetById(int id);
   
    Departamento Add(Departamento departamento);

    Departamento? Update(int id ,Departamento departamento);

    bool Delete(int id);
   
}