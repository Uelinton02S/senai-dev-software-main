using MinhaApi.Models;

namespace MinhaApi.Services;


public interface IVendaService
{
    Venda Add(int id,Venda venda);
    Venda? Update(int idclientes,int idprodutos,Venda venda);
    Venda?GetById(int id,Venda venda);
    Venda Create (int id, Venda venda);
}