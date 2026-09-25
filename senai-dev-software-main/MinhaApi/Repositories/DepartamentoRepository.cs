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
         string sql = @"SELECT id, ,idfornecedores,iddepartaemento, nome,email,descricao,telefone       
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
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            SELECT 
                id,
                nome,
                email,
                descricao,
                telefone,
                idfornecedores,
                iddepartamento,
            
            FROM departamento
            WHERE id = @id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Departamento
            {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    email = reader.GetString("email"),
                    idfornecedores = reader.GetInt32("idfornecedores"),
                    iddepartamento = reader.GetInt32("iddepartamento"),
                    telefone = reader.GetString("telefone"),
                    Ativo = reader.GetBoolean("ativo")
            
            };
        }

        return null;
    }

    public void Add(Departamento departamento)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            INSERT INTO departamento 
                (nome, iddepartamento,@descricao idforncedores, email,telefone)
            VALUES 
                (@nome, @idfornecedores,@descricao @iddepartamento, @telefone)";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Nome", departamento.Nome);
        cmd.Parameters.AddWithValue("@Email", departamento.email);
        cmd.Parameters.AddWithValue("@telefone", departamento.telefone);
        cmd.Parameters.AddWithValue("@idfornecedores", departamento.idfornecedores);
        cmd.Parameters.AddWithValue("@iddepartamento", departamento.iddepartamento);
        cmd.Parameters.AddWithValue("@descricao", departamento.descricao);

        cmd.ExecuteNonQuery();

        departamento.Id = (int)cmd.LastInsertedId;
    }

    public void Update(Departamento departamento)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"
            UPDATE departamento
            SET 
                nome = @nome,
                iddepartamento = @iddepartamento
                idprodutos = @idfornecedor,
                descricao = @descricao
                email = @email

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


