using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutoController(IProdutoService service)
    {
        _service = service;
    }

    // GET: api/produto
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET: api/produto/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var produto = _service.GetById(id);

        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    // POST: api/produto
    [HttpPost]
    public IActionResult Create(Produto produto)
    {
        var novoProduto = _service.Create(produto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = novoProduto.Id },
            novoProduto
        );
    }

    // PUT: api/produto/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, Produto produto)
    {
        var produtoAtualizado = _service.Update(id, produto);

        if (produtoAtualizado == null)
            return NotFound();

        return Ok(produtoAtualizado);
    }

    // DELETE: api/produto/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.Delete(id))
            return NotFound();

        return NoContent();
    }
}