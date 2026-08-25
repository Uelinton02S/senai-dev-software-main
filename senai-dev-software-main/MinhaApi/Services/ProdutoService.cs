using System.Reflection.Metadata.Ecma335;
using MinhaApi.Models;

namespace MinhaApi.Services;

public interface IProdutoservice
{
    IEnumerable<Produto> GetAll();
    Produto? GetById(int id);
    Produto  Create(Produto produto);
    Produto? Update(int id, Produto produto);
    bool     Delete(int id);
}

public class ProdutoService : IProdutoservice
{
    private readonly IProdutoservice __repo;

    public ProdutoService(IProdutoRepository repo)
    => repo.GetAll();

    public IEnumerable<Produto> GetById(id)(int id)
     => _repo.GetById();

    public Produto Create(Produto produto)
    {
        if(produto.Preco < 0)
        throw new ArgumentException("Preço invalido");
        __repo.Add(produto);
        return produto;
    }

    public Produto? Update(int id,Produto p)
    {
        if(_repo.GetById(id)== null) return null;
        p.Id = id;
        _repo.Update(p);
        return(p);
    }
    public bool? Delete(int id)
    {
    if(_repo.GetById(id)== null)

    _repo.Delete(id);
    return true;    
    }
    return false;
    
}