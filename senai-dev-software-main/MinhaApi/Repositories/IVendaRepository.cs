using MinhaApi.Models;

namespace  MinhaApi.Repositories;

public interface IVendaRepository
{
    IEnumerable<Venda> GetAll();
    void Add(Venda venda);
    void Update(Venda venda);
}


