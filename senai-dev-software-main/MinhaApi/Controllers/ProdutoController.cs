using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;
   
   // GET/ api/produto
   public ProdutoController(
    IProdutoService service)
    => _service = service;
   
   // GET/ api/produto
   [HttpGet]
   public IActionResult getAll()
    {
        var produtos = _service.GetAll();
        return Ok(produtos);
        if(Produto == null)
             return NotFound();
        return Ok(produto);
    
    
    }
    //POST/api/produto

        [HttpPost]
        public IActionResult Create(
            [FromBody] Produto produto)

        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var criado = _service.Create(produto);
                
                return CreatedAtAction(
                    nameof(getById),
                    new {id = criado.id},
                    criado);
            {
                "nome": "notebook",
                "preco": 2500.00,
                "estoque":,10
            }
        }
    // PUT /api/produto/1
        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            [FromBody] Produto protudo)
        {
            var atualizado = _service.Update(id, produto);
            if(atualizado == null)
            return NotFound();
            
            return Ok(atualizado); 
        }
        
// DELETE /api/produto/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletado = _service.Delete(id);

            if (!deletado)
                return NotFound();

            return NoContent();
        }
}