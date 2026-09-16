using MinhaApi.Models;
using MinhaApi.Repositories;
using MySqlConnector;

public class VendaRepository : IVendaRepository
{
    private readonly string _connectionString;

    public VendaRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public IEnumerable<Venda> GetAll()
    {
        var lista = new List<Venda>();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            SELECT 
                id,
                data_venda,
                idclientes,
                idprodutos,
                quantidade
            FROM vendas";

        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Venda
            {
                id = reader.GetInt32("id"),
                data_venda = reader.GetDateTime("data_venda"),
                idclientes = reader.GetInt32("idclientes"),
                idprodutos = reader.GetInt32("idprodutos"),
                quantidade = reader.GetInt32("quantidade")
            });
        }

        return lista;
    }

    public Venda? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            SELECT 
                id,
                data_venda,
                idclientes,
                idprodutos,
                quantidade
            FROM vendas
            WHERE id = @id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Venda
            {
                id = reader.GetInt32("id"),
                data_venda = reader.GetDateTime("data_venda"),
                idclientes = reader.GetInt32("idclientes"),
                idprodutos = reader.GetInt32("idprodutos"),
                quantidade = reader.GetInt32("quantidade")
            };
        }

        return null;
    }

    public void Add(Venda venda)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            INSERT INTO vendas 
                (data_venda, idclientes, idprodutos, quantidade)
            VALUES 
                (@Data, @idclientes, @idprodutos, @Quantidade)";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Data", venda.data_venda);
        cmd.Parameters.AddWithValue("@idclientes", venda.idclientes);
        cmd.Parameters.AddWithValue("@idprodutos", venda.idprodutos);
        cmd.Parameters.AddWithValue("@Quantidade", venda.quantidade);

        cmd.ExecuteNonQuery();

        venda.id = (int)cmd.LastInsertedId;
    }

    public void Update(Venda venda)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            UPDATE vendas 
            SET 
                data_venda = @Data,
                idclientes = @idclientes,
                idprodutos = @idprodutos,
                quantidade = @Quantidade
            WHERE id = @id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Data", venda.data_venda);
        cmd.Parameters.AddWithValue("@idclientes", venda.idclientes);
        cmd.Parameters.AddWithValue("@idprodutos", venda.idprodutos);
        cmd.Parameters.AddWithValue("@Quantidade", venda.quantidade);
        cmd.Parameters.AddWithValue("@id", venda.id);

        cmd.ExecuteNonQuery();
    }
}
