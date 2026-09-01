using MinhaApi.Models;

namespace MinhaApi.Repositories;

public interface IProdutoRepository
{
    IEnumerable<Produto> GetAll();
    Produto? GetById(int id);
    void Add(Produto produto);
    void Update(Produto produto);
    void Delete(int id);
}

public class ProdutoRepository : IProdutoRepository
{
    private static List<Produto> _db = new()
    {
        new Produto
        {
            Id = 1,
            Nome = "Notebook",
            Preco = 2500m,
            Estoque = 10
        },

        new Produto
        {
            Id = 2,
            Nome = "Mouse",
            Preco = 89.90m,
            Estoque = 50
        }
    };

    public IEnumerable<Produto> GetAll()
    {
      var lista = new List<Produto>();
      using var conn = new MySqlConnection(_connectionString);
      conn.Open();

      string sql = "SELECT id, nome, preco, estoque, ativo FROM produtos";
      using var cmd = new MySqlCommand(sql, conn);
      using var reader = cmd.ExecuteReader();

      while (reader.Read()) {
          lista.Add(new Produto {
              Id = reader.GetInt32("id"),
              Nome = reader.GetString("nome"),
              Preco = reader.GetDecimal("preco"),
              Estoque = reader.GetInt32("estoque"),
              Ativo = reader.GetBoolean("ativo")
          });
      }
      return lista;
  }


    public Produto? GetById(int id)
        => _db.FirstOrDefault(p => p.Id == id);

    public void Add(Produto p)
    {
        p.Id = _db.Any() ? _db.Max(x => x.Id) + 1 : 1;
        _db.Add(p);
    }

    public void Update(Produto p)
    {
        var i = _db.FindIndex(x => x.Id == p.Id);

        if (i >= 0)
            _db[i] = p;
    }

    public void Delete(int id)
        => _db.RemoveAll(p => p.Id == id);
}
