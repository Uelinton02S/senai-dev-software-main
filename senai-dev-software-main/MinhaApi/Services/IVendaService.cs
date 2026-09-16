using MinhaApi.Models;

namespace MinhaApi.Services;

public interface IVendaService
{
    Venda Create(Venda venda);

    Venda? Update(int id, Venda venda);

    Venda? GetById(int id);
}
