using MinhaApi.Models;
using MinhaApi.Repository;

namespace MinhaApi.Services;

public interface IVendaService
{
    Venda RealizarVenda(Venda venda);
}

public class VendaService : IVendaService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IVendaRepository _vendaRepository;

    public VendaService(
        IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository,
        IVendaRepository vendaRepository)
    {
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _vendaRepository = vendaRepository;
    }

    public Venda RealizarVenda(Venda venda)
    {
        
        var cliente = _clienteRepository.GetById(venda.ClienteId);

        if (cliente == null || !cliente.Ativo)
        {
            throw new Exception("Cliente não encontrado ou está inativo.");
        }

        
        var produto = _produtoRepository.GetById(venda.ProdutoId);

        if (produto == null || !produto.Ativo)
        {
            throw new Exception("Produto não encontrado ou está inativo.");
        }

        
        if (produto.Estoque < venda.Quantidade)
        {
            throw new Exception("Estoque insuficiente.");
        }

        venda.ValorTotal = produto.Preco * venda.Quantidade;
        venda.DataVenda = DateTime.Now;

        
        produto.Estoque -= venda.Quantidade;

        _produtoRepository.Update(produto.Id, produto);

        
        return _vendaRepository.Add(venda);
    }
}
