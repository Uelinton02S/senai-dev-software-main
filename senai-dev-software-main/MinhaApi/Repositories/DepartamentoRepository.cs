using MinhaApi.Models;
using MySqlConnector;
namespace MinhaApi.Repositories;


public class DepartamentoRepository : IDepartamentoRepository
{
     private readonly string _connectionstring;
     
     public DepartamentoRepository(IConfiguration config)
     => _connectionstring = config.GetConnectionString("DefaultConnection")!;

     public IEnumerable<Departamento> GetAll()
     {
          var lista = new List<Departamento>();
          using var conn = new MySqlConnection(_connectionstring);
          conn.Open();
         string sql = @"SELECT id,idfornecedores,iddepartamento,nome,email,descricao,telefone       
               FROM departamento";
          using var cmd = new MySqlCommand(sql,conn);
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
               lista.Add(new Departamento
               {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    email = reader.GetString("email"),
                    descricao = reader.GetString("descricao"),
                    idfornecedores = reader.GetInt32("idfornecedores"),
                    iddepartamento = reader.GetInt32("iddepartamento"),
                    telefone = reader.GetString("telefone"),
                    Ativo = reader.GetBoolean("ativo")
               });
          }

          return lista;

     }
      public Departamento? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionstring);
        conn.Open();

        string sql = """
            SELECT id,idfornecedores,iddepartamento,nome,email,descricao,telefone,ativo
            FROM departamento
            WHERE id = @Id
            """;

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        return new Departamento
        {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    email = reader.GetString("email"),
                    descricao = reader.GetString("descricao"),
                    idfornecedores = reader.GetInt32("idfornecedores"),
                    iddepartamento = reader.GetInt32("iddepartamento"),
                    telefone = reader.GetString("telefone"),
                    Ativo = reader.GetBoolean("ativo")
        };
    }

    public void Add(Departamento departamento)
    {
        using var conn = new MySqlConnection(_connectionstring);
        conn.Open();

        string sql = @"
            INSERT INTO departamento 
                (nome, iddepartamento,idfornecedores,descricao,email,telefone)
            VALUES 
                (@nome, @idfornecedores,@iddepartamento, @descricao, @email, @telefone)";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@nome", departamento.Nome);
        cmd.Parameters.AddWithValue("@email", departamento.email);
        cmd.Parameters.AddWithValue("@telefone", departamento.telefone);
        cmd.Parameters.AddWithValue("@idfornecedores", departamento.idfornecedores);
        cmd.Parameters.AddWithValue("@iddepartamento", departamento.iddepartamento);
        cmd.Parameters.AddWithValue("@descricao", departamento.descricao);

        cmd.ExecuteNonQuery();

        departamento.Id = (int)cmd.LastInsertedId;
    }

    public void Update(Departamento departamento)
    {
        using var conn = new MySqlConnection(_connectionstring);
        conn.Open();

        string sql = @"
            UPDATE departamento
            SET 
                nome = @nome,
                iddepartamento = @iddepartamento,
                idfornecedores = @idfornecedores,
                descricao = @descricao,
                email = @email,
                telefone = @telfone,
                ativo = @ativo

            WHERE id = @id";

        using var cmd = new MySqlCommand(sql, conn);

        
        cmd.Parameters.AddWithValue("@Nome", departamento.Nome);
        cmd.Parameters.AddWithValue("@Email", departamento.email);
        cmd.Parameters.AddWithValue("@telefone", departamento.telefone);
        cmd.Parameters.AddWithValue("@idfornecedores", departamento.idfornecedores);
        cmd.Parameters.AddWithValue("@iddepartamento", departamento.iddepartamento);
        cmd.Parameters.AddWithValue("@descricao", departamento.descricao);


        cmd.ExecuteNonQuery();
    }
        public void Delete(int id)
     {
          using var conn = new MySqlConnection(_connectionstring);
          conn.Open();
          string sql = @"DELETE FROM departamento WHERE id = @Id";
          using var cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue(@"Id", id);
          cmd.ExecuteNonQuery();
     }
}


