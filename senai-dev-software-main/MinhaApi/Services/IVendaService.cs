using MinhaApi.Models;
using MinhaApi.Repositories;
namespace MinhaApi.Services;


public interface IVendaService
{
    IEnumerable<Venda> GetAll();
    void Add(Venda venda);
    Venda Update(Venda venda);
}