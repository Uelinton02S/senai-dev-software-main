
using MinhaApi.Models;
using MinhaApi.Repositories;
using Microsoft.AspNetCore.Http;

namespace MinhaApi.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repo;
    private readonly IFotoService _fotoService;

    public ProdutoService(
        IProdutoRepository repo,
        IFotoService fotoService)
    {
        _repo = repo;
        _fotoService = fotoService;
    }

    // Listar produtos
    public List<Produto> GetAll()
    {
        var produtos = _repo.GetAll();

        return produtos.Select(p => new Produto
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Estoque = p.Estoque,
            Ativo = p.Ativo,
            Foto = p.Foto,
            FotoBase64 = _fotoService.LerBase64(p.Foto)
        }).ToList();
    }

    // Buscar produto por ID
    public Produto? GetById(int id)
    {
        var produto = _repo.GetById(id);

        if (produto == null)
            return null;

        return new Produto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Estoque = produto.Estoque,
            Ativo = produto.Ativo,
            Foto = produto.Foto,
            FotoBase64 = _fotoService.LerBase64(produto.Foto)
        };
    }

    // Cadastrar produto
    public Produto Create(Produto produto)
    {
        if (produto.Preco <= 0)
            throw new ArgumentException("Preço inválido.");

        _repo.Add(produto);

        return produto;
    }

    // Atualizar produto
    public Produto? Update(int id, Produto produto)
    {
        if (_repo.GetById(id) == null)
            return null;

        if (produto.Preco <= 0)
            throw new ArgumentException("Preço inválido.");

        produto.Id = id;

        _repo.Update(produto);

        return produto;
    }

    // Excluir produto
    public bool Delete(int id)
    {
        var produto = _repo.GetById(id);

        if (produto == null)
            return false;

        if (!string.IsNullOrEmpty(produto.Foto))
            _fotoService.Deletar(produto.Foto);

        _repo.Delete(id);

        return true;
    }

    // Salvar foto
    public async Task<string> SalvarFoto(int id, IFormFile arquivo)
    {
        var produto = _repo.GetById(id)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        // Primeiro salva a nova foto
        var nome = await _fotoService.SalvarAsync(arquivo);

        try
        {
            // Atualiza o banco
            _repo.AtualizarFoto(id, nome);
        }
        catch
        {
            // Se falhar no banco, remove a nova imagem
            _fotoService.Deletar(nome);
            throw;
        }

        // Depois remove a foto antiga
        if (!string.IsNullOrEmpty(produto.Foto))
            _fotoService.Deletar(produto.Foto);

        return _fotoService.LerBase64(nome)!;
    }

    // Remover foto
    public void RemoverFoto(int id)
    {
        var produto = _repo.GetById(id)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        if (string.IsNullOrEmpty(produto.Foto))
            return;

        _repo.AtualizarFoto(id, null);

        _fotoService.Deletar(produto.Foto);
    }

    IEnumerable<Produto> IProdutoService.GetAll()
    {
        return GetAll();
    }
}
