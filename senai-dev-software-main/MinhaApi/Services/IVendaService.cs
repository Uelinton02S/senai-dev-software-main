using MinhaApi.Models;
using MinhaApi.Repositories;
namespace MinhaApi.Services;


public interface IVendaService
{
    IEnumerable<Venda> GetAll();
    Venda? GetById(int id);
    Venda Update(Venda venda);
}