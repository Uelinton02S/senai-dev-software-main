using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private static readonly List<Dictionary<string, object>> Fornecedores = new()
        {
            new Dictionary<string, object>
            {
                ["id"] = 1,
                ["nome"] = "Fornecedor A",
                ["telefone"] = "(11) 99999-1111",
                ["email"] = "contato@fornecedora.com.br",
                ["ativo"] = true
            },
            new Dictionary<string, object>
            {
                ["id"] = 2,
                ["nome"] = "Fornecedor B",
                ["telefone"] = "(21) 98888-2222",
                ["email"] = "vendas@fornecedorb.com.br",
                ["ativo"] = true
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Dictionary<string, object>>> Get()
        {
            return Ok(Fornecedores);
        }

        [HttpGet("{id}")] 
        public ActionResult<Dictionary<string, object>> GetById(int id)
        {
            var fornecedor = Fornecedores.FirstOrDefault(f => (int)f["id"] == id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        [HttpPost]
        public ActionResult<Dictionary<string, object>> Post([FromBody] Dictionary<string, object> fornecedor)
        {
            if (fornecedor == null)
            {
                return BadRequest();
            }

            var nextId = Fornecedores.Count == 0 ? 1 : Fornecedores.Max(f => (int)f["id"]) + 1;
            fornecedor["id"] = nextId;

            Fornecedores.Add(fornecedor);

            return CreatedAtAction(nameof(GetById), new { id = nextId }, fornecedor);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Dictionary<string, object> fornecedorAtualizado)
        {
            if (fornecedorAtualizado == null)
            {
                return BadRequest();
            }

            var fornecedor = Fornecedores.FirstOrDefault(f => (int)f["id"] == id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            fornecedor["nome"] = fornecedorAtualizado.ContainsKey("nome") ? fornecedorAtualizado["nome"] : fornecedor["nome"];
            fornecedor["telefone"] = fornecedorAtualizado.ContainsKey("telefone") ? fornecedorAtualizado["telefone"] : fornecedor["telefone"];
            fornecedor["email"] = fornecedorAtualizado.ContainsKey("email") ? fornecedorAtualizado["email"] : fornecedor["email"];
            fornecedor["ativo"] = fornecedorAtualizado.ContainsKey("ativo") ? fornecedorAtualizado["ativo"] : fornecedor["ativo"];

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var fornecedor = Fornecedores.FirstOrDefault(f => (int)f["id"] == id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            Fornecedores.Remove(fornecedor);

            return NoContent();
        }
    }
}
