using MinhaApi.Models;
using MinhaApi.Repositories;

namespace MinhaApi.Services;

public interface IProdutoService
{
IEnumerable<Produto> GetAll();
Produto? GetById(int id);
Produto Create(Produto produto);
Produto? Update(int id, Produto produto);
bool Delete(int id);
}

public class ProdutoService : IProdutoService
{
private readonly IProdutoRepository _repo;


public ProdutoService(IProdutoRepository repo)
{
    _repo = repo;
}

public IEnumerable<Produto> GetAll()
{
    return _repo.GetAll();
}

public Produto? GetById(int id)
{
    return _repo.GetById(id);
}

public Produto Create(Produto produto)
{
    if (produto.Preco < 0)
        throw new ArgumentException("Preço inválido");

    _repo.Add(produto);

    return produto;
}

public Produto? Update(int id, Produto produto)
{
    if (_repo.GetById(id) == null)
        return null;

    produto.Id = id;
    _repo.Update(produto);

    return produto;
}

public bool Delete(int id)
{
    if (_repo.GetById(id) == null)
        return false;

    _repo.Delete(id);

    return true;
}


}
