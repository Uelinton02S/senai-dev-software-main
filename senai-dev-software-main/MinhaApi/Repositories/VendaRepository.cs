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
    
            string sql = "SELECT id, data_venda, idclientes, idprodutos, quantidade FROM vendas";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
    
            while (reader.Read())
            {
                lista.Add(new Venda{
                    
                    id = reader.GetInt32("id"),
                    data_venda = reader.GetDateTime("data_venda"),
                    idclientes = reader.GetInt32("idclientes"),
                    idprodutos = reader.GetInt32("idprodutos"),
                    quantidade = reader.GetInt32("quantidade")
                });
            }
            return lista;
        }
       public void Add(Venda venda) {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        string sql = @"INSERT INTO vendas (data,idclientes, idprodutos, quantidade) 
                       VALUES (@Data, @idclientes, @idprodutos, @Quantidade);
                       SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Data", venda.data_venda);
        cmd.Parameters.AddWithValue("@idclientes", venda.idclientes);
        cmd.Parameters.AddWithValue("@idprodutos", venda.idprodutos);
        cmd.Parameters.AddWithValue("@Quantidade", venda.quantidade);
        cmd.ExecuteNonQuery();
    }
      
      public void Update(Venda venda)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        string sql = @"UPDATE vendas SET data_venda = @Data, idclientes = @idclientes, idprodutos = @idprodutos, quantidade = @Quantidade WHERE id = @id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Data", venda.data_venda);
        cmd.Parameters.AddWithValue("@idclientes", venda.idclientes);
        cmd.Parameters.AddWithValue("@idprodutos", venda.idprodutos);
        cmd.Parameters.AddWithValue("@Quantidade", venda.quantidade);
        cmd.Parameters.AddWithValue("@id", venda.id);
        cmd.ExecuteNonQuery();
    }

    IEnumerable<Venda> IVendaRepository.GetAll()
    {
        return GetAll();
    }
}