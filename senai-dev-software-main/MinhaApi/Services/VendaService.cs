using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;

public class VendaService : IVendaService
{
    private readonly IClienteRepository _ClienteRepository;
    private readonly IProdutoRepository _ProdutoRepository;
    private readonly IVendaRepository _VendaRepository;

    public VendaService(
        IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository,
        IVendaRepository vendaRepository)
    {
        _ClienteRepository = clienteRepository;
        _ProdutoRepository = produtoRepository;
        _VendaRepository = vendaRepository;
    }

        public Venda Create (Venda venda){
            
            var cliente = _ClienteRepository.GetById(venda.idclientes);

            if (cliente == null || !cliente.Ativo)
            {
                throw new Exception("Cliente não encontrado ou está inativo.");
            }

            var produto = _ProdutoRepository.GetById(venda.idprodutos);

            if (produto == null || !produto.Ativo)
            {
                throw new Exception("Produto não encontrado ou está inativo.");
            }

            
            if (produto.Estoque < venda.quantidade)
            {
                throw new Exception("Estoque insuficiente.");
            }

            venda.ValorTotal = produto.Preco * venda.quantidade;
            venda.data_venda = DateTime.Now;
            public Venda Add(int id, Venda venda)
            {
                throw new NotImplementedException();
            }

            public Venda? Update(int idclientes, int idprodutos, Venda venda)
            {
                throw new NotImplementedException();
            }

            public Venda? GetById(int id, Venda venda)
            {
                throw new NotImplementedException();
            }
        }
}




