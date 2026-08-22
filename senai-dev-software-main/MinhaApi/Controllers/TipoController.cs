using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly TipoService _service;
   
   // GET/ api/produto
   public TipoController(
    ITipoService service)
    => _service = service;
   
   // GET/ api/produto
   [HttpGet]
   public IActionResult getAll()
    {
        var tipo = _service.GetAll();
        return Ok(tipo);
        if(tipo == null)
             return NotFound();
        return Ok(tipo);
    
    
    }
    //POST/api/produto

        [HttpPost]
        public IActionResult Create(
            [FromBody] Tipo tipo)

        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var criado = _service.Create(tipo);
                
                return CreatedAtAction(
                    nameof(getById),
                    new {id = criado.id},
                    criado);
            
        }
    // PUT /api/produto/1
        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            [FromBody] Tipo tipo)
        {
            var atualizado = _service.Update(id, tipo);
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