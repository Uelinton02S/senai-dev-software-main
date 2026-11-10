using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


[Authorize]
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
public IActionResult Create([FromBody] Produto produto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var novoProduto = _service.Create(produto);

    return CreatedAtAction(
        nameof(GetById),
        new { id = novoProduto.Id },
        novoProduto
    );
}

// PUT: api/produto/1
[HttpPut("{id}")]
public IActionResult Update(int id, [FromBody] Produto produto)
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


[HttpPost("{id}/foto")]
public async Task<IActionResult> UploadFoto(
    int id, IFormFile arquivo)
{
    if (arquivo == null || arquivo.Length == 0)
        return BadRequest(new { message = "Nenhum arquivo enviado." });

    try
    {
        var fotoBase64 = await _service.SalvarFoto(id, arquivo);
        // Retorna a data URL pronta para o React usar no <img src>
        return Ok(new { fotoBase64 });
    }
    catch (KeyNotFoundException ex)
        { return NotFound(new { message = ex.Message }); }
    catch (ArgumentException ex)
        { return BadRequest(new { message = ex.Message }); }
}

[HttpDelete("{id}/foto")]
public IActionResult RemoverFoto(int id)
{
    try
    {
        _service.RemoverFoto(id);
        return NoContent(); // 204 — sem corpo
    }
    catch (KeyNotFoundException ex)
        { return NotFound(new { message = ex.Message }); }
}
}
