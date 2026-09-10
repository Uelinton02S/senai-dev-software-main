using MinhaApi.Models;
using MySqlConnector;
using MinhaApi.Repositories;

public class VendaRespository : IVendaRepository
{
    private readonly string _connectionString;

    public VendaRespository(IConfiguration config)
=> _connectionString = config.GetConnectionString("DefaultConnection")!;
    
    private static List<Venda> _db = new()
    {
        new Venda { id=1, data_venda=DateTime.Now, idclientes=1, idprodutos=1, quantidade=2 },
        new Venda { id=2, data_venda=DateTime.Now, idclientes=2, idprodutos=2, quantidade=1 }
    };
    IEnumerable<Venda> GetAll()
        {
            var lista = new List<Venda>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
    
            string sql = "SELECT id, data, cliente_id, produto_id, quantidade FROM vendas";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
    
            while (reader.Read())
            {
                lista.Add(new Venda{
                    
                    id = reader.GetInt32("id"),
                    data_venda = reader.GetDateTime("data"),
                    idclientes = reader.GetInt32("cliente_id"),
                    idprodutos = reader.GetInt32("produto_id"),
                    quantidade = reader.GetInt32("quantidade")
                });
            }
            return lista;
        }
        public Venda? GetById(int id) => _db.FirstOrDefault(v => v.id == id);
      
      public void Update(Venda venda)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        string sql = @"UPDATE vendas SET data_venda = @Data, cliente_id = @ClienteId, produto_id = @ProdutoId, quantidade = @Quantidade WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Data", venda.data_venda);
        cmd.Parameters.AddWithValue("@ClienteId", venda.idclientes);
        cmd.Parameters.AddWithValue("@ProdutoId", venda.idprodutos);
        cmd.Parameters.AddWithValue("@Quantidade", venda.quantidade);
        cmd.Parameters.AddWithValue("@Id", venda.id);

        cmd.ExecuteNonQuery();
    }

    IEnumerable<Venda> IVendaRepository.GetAll()
    {
        return GetAll();
    }
}