using MinhaApi.Models;
using MySqlConnector;
namespace MinhaApi.Repositories;

public class FornecedoresRepository : IFornecedoresRepository
{
     private readonly string _connectionstring;
     
     public FornecedoresRepository(IConfiguration config)
     => _connectionstring = config.GetConnectionString("DefaultConnection")!;

     public IEnumerable<Fornecedores> GetAll()
     {
          var lista = new List<Fornecedores>();
          using var conn = new MySqlConnection(_connectionstring);
          conn.Open();
          string sql = "SELECT id, nome, cnpj, ativo FROM fornecedores";
          using var cmd = new MySqlCommand(sql,conn);
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
               lista.Add(new Fornecedores
               {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Cnpj = reader.GetString("cnpj"),
                    Data_venda = reader.GetDateTime("data_venda"),
                    Cep = reader.GetInt16("cep"),
                    numero = reader.GetInt16("numero"),
                    Complemento = reader.GetString("complemento"),
                    Celular = reader.GetString("celular"),
                    telefone = reader.GetString("telefone,"),
                    Ativo = reader.GetBoolean("ativo")
               });
          }

          return lista;
     }

     public Fornecedores? GetById(int id)
     {
          using var conn = new MySqlConnection(_connectionstring);
          conn.Open();
          string sql = "SELECT id, nome, email, cnpj, data_venda, cep, numero, complemento, celular, telefone, ativo FROM fornecedores WHERE id = @id";
          using var cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue("@id", id);
          using var reader = cmd.ExecuteReader();
          if (reader.Read())
          {
               return new Fornecedores
               {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    email = reader.GetString("email"),
                    Cnpj = reader.GetString("cnpj"),
                    Data_venda = reader.GetDateTime("data_venda"),
                    Cep = reader.GetInt16("cep"),
                    numero = reader.GetInt16("numero"),
                    Complemento = reader.GetString("complemento"),
                    Celular = reader.GetString("celular"),
                    telefone = reader.GetString("telefone"),
                    Ativo = reader.GetBoolean("ativo")
               };
          }

          return null;
     }
     public void Create(Fornecedores fornecedores)
     {
          using var conn = new MySqlConnection(_connectionstring);
          conn.Open();
          string sql = @"INSERT INTO fornecedores (nome,email,cnpj,data_venda,cep,numero,complemento,celular,telefone,ativo)
                         VALUES (@Nome,@Email,@Cnpj,@Data_venda,@Cep,@Numero,@Complemento,@Celular,@Telefone,@Ativo);
                           SELECT LAST_INSERT_ID();";
          using var cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue("@Nome", fornecedores.Nome);
          cmd.Parameters.AddWithValue("@Email", fornecedores.email);
          cmd.Parameters.AddWithValue("@Cnpj", fornecedores.Cnpj);
          cmd.Parameters.AddWithValue("@Data_venda", fornecedores.Data_venda);
          cmd.Parameters.AddWithValue("@Cep", fornecedores.Cep);
          cmd.Parameters.AddWithValue("@Numero", fornecedores.numero);
          cmd.Parameters.AddWithValue("@Complemento", fornecedores.Complemento);
          cmd.Parameters.AddWithValue("@Celular", fornecedores.Celular);
          cmd.Parameters.AddWithValue("@Telefone", fornecedores.telefone);
          cmd.Parameters.AddWithValue("@Ativo", fornecedores.Ativo);
          cmd.ExecuteNonQuery();
     }
     public void Update(Fornecedores fornecedores)
     {
          using var conn = new MySqlConnection(_connectionstring);
          conn.Open();
          string sql = @"UPDATE fornecedores SET nome = @Nome, email = @Email, cnpj = @Cnpj, data_venda = @Data_venda, cep = @Cep, numero = @Numero, complemento = @Complemento, celular = @Celular, telefone = @Telefone, ativo = @Ativo WHERE id = @Id";
          using var cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue("@Id", fornecedores.Id);
          cmd.Parameters.AddWithValue("@Nome", fornecedores.Nome);
          cmd.Parameters.AddWithValue("@Email", fornecedores.email);
          cmd.Parameters.AddWithValue("@Cnpj", fornecedores.Cnpj);
          cmd.Parameters.AddWithValue("@Data_venda", fornecedores.Data_venda);
          cmd.Parameters.AddWithValue("@Cep", fornecedores.Cep);
          cmd.Parameters.AddWithValue("@Numero", fornecedores.numero);
          cmd.Parameters.AddWithValue("@Complemento", fornecedores.Complemento);
          cmd.Parameters.AddWithValue("@Celular", fornecedores.Celular);
          cmd.Parameters.AddWithValue("@Telefone", fornecedores.telefone);
          cmd.Parameters.AddWithValue("@Ativo", fornecedores.Ativo);
          cmd.ExecuteNonQuery();
     }

     public void Delete(int id)
     {
          using var conn = new MySqlConnection(_connectionstring);
          conn.Open();
          string sql = @"DELETE FROM fornecedores WHERE id = @Id";
          using var cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue(@"Id", id);
          cmd.ExecuteNonQuery();
     }

}