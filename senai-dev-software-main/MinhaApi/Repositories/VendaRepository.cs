
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
                clientes_id,
                produtos_id,
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
                idclientes = reader.GetInt32("clientes_id"),
                idprodutos = reader.GetInt32("produtos_id"),
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
                clientes_id,
                produtos_id,
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
                idclientes = reader.GetInt32("clientes_id"),
                idprodutos = reader.GetInt32("produtos_id"),
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
                (data_venda, clientes_id, produtos_id, quantidade, valor_total)
            VALUES 
                (@Data, @idclientes, @idprodutos, @Quantidade, @ValorTotal)";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Data", venda.data_venda);
        cmd.Parameters.AddWithValue("@idclientes", venda.idclientes);
        cmd.Parameters.AddWithValue("@idprodutos", venda.idprodutos);
        cmd.Parameters.AddWithValue("@Quantidade", venda.quantidade);
        cmd.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);

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
                clientes_id = @idclientes,
                produtos_id = @idprodutos,
                quantidade = @Quantidade,
                valor_total = @ValorTotal
            WHERE id = @id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Data", venda.data_venda);
        cmd.Parameters.AddWithValue("@idclientes", venda.idclientes);
        cmd.Parameters.AddWithValue("@idprodutos", venda.idprodutos);
        cmd.Parameters.AddWithValue("@Quantidade", venda.quantidade);
        cmd.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
        cmd.Parameters.AddWithValue("@id", venda.id);

        cmd.ExecuteNonQuery();
    }
}
