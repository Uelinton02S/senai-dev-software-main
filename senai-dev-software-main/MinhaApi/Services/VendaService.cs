
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

    public IEnumerable<Venda> GetAll()
    {
        return _VendaRepository.GetAll();
    }

    public Venda Create(Venda venda)
    {
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

        if (venda.quantidade <= 0)
        {
            throw new Exception("A quantidade deve ser maior que zero.");
        }

        if (produto.Estoque < venda.quantidade)
        {
            throw new Exception("Estoque insuficiente.");
        }

        venda.ValorTotal = produto.Preco * venda.quantidade;
        venda.data_venda = DateTime.Now;

        produto.Estoque -= venda.quantidade;

        _ProdutoRepository.Update(produto);
        _VendaRepository.Add(venda);

        return venda;
    }

    public Venda? GetById(int id)
    {
        return _VendaRepository.GetById(id);
    }

    public Venda? Update(int id, Venda venda)
    {
        var vendaExistente = _VendaRepository.GetById(id);

        if (vendaExistente == null)
        {
            return null;
        }

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

        if (venda.quantidade <= 0)
        {
            throw new Exception("A quantidade deve ser maior que zero.");
        }

        venda.id = id;
        venda.data_venda = DateTime.Now;
        venda.ValorTotal = produto.Preco * venda.quantidade;

        _VendaRepository.Update(venda);

        return venda;
    }
}

