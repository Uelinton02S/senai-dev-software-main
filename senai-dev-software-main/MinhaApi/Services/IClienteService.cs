using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;
public interface IClienteService
{
    IEnumerable<Cliente> GetAll();
    Cliente? GetById(int id);
    Cliente Create(Cliente cliente);
    Cliente? Update(int id, Cliente cliente);
    bool Delete(int id);
}