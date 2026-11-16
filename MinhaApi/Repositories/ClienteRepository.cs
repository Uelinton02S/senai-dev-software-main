
using MinhaApi.Models;
using MySqlConnector;

namespace MinhaApi.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public IEnumerable<Cliente> GetAll()
    {
        var lista = new List<Cliente>();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        const string sql = "SELECT id, nome, email, cpf, ativo FROM clientes";

        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Cliente
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Email = reader.GetString("email"),
                Cpf = Convert.ToString(reader["cpf"]) ?? string.Empty,
                Ativo = reader.GetBoolean("ativo")
            });
        }

        return lista;
    }

    public Cliente? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        const string sql = @"
            SELECT id, nome, email, cpf, ativo
            FROM clientes
            WHERE id = @Id
            LIMIT 1";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new Cliente
        {
            Id = reader.GetInt32("id"),
            Nome = reader.GetString("nome"),
            Email = reader.GetString("email"),
            Cpf = Convert.ToString(reader["cpf"]) ?? string.Empty,
            Ativo = reader.GetBoolean("ativo")
        };
    }

    public void Add(Cliente c)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        const string sql = @"
            INSERT INTO clientes (nome, email, cpf, ativo)
            VALUES (@Nome, @Email, @Cpf, @Ativo);
            SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Nome", c.Nome);
        cmd.Parameters.AddWithValue("@Email", c.Email);
        cmd.Parameters.AddWithValue("@Cpf", c.Cpf);
        cmd.Parameters.AddWithValue("@Ativo", c.Ativo);

        c.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Update(Cliente c)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        const string sql = @"
            UPDATE clientes
            SET nome = @Nome,
                email = @Email,
                cpf = @Cpf,
                ativo = @Ativo
            WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", c.Id);
        cmd.Parameters.AddWithValue("@Nome", c.Nome);
        cmd.Parameters.AddWithValue("@Email", c.Email);
        cmd.Parameters.AddWithValue("@Cpf", c.Cpf);
        cmd.Parameters.AddWithValue("@Ativo", c.Ativo);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        const string sql = "DELETE FROM clientes WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
    }
}
