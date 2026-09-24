using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
private readonly IFornecedoresService _service;


public FornecedoresController(IFornecedoresService service)
{
    _service = service;
}

// GET: api/forncedor
[HttpGet]
public IActionResult GetAll()
{
    return Ok(_service.GetAll());
}

// GET: api/forncedor/1
[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var fornecedor = _service.GetById(id);

    if (fornecedor == null)
        return NotFound();

    return Ok(fornecedor);
}

// POST: api/forncedor
[HttpPost]
public IActionResult Create([FromBody] Fornecedores fornecedores)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var novoProduto = _service.Create(fornecedores);

    return CreatedAtAction(
        nameof(GetById),
        new { id = novoProduto.Id },
        novoProduto
    );
}

// PUT: api/forncedor/1
[HttpPut("{id}")]
public IActionResult Update(int id, [FromBody] Fornecedores fornecedores)
{
    var fornecedorAtualizado = _service.Update(id, fornecedores);

    if (fornecedorAtualizado == null)
        return NotFound();

    return Ok(fornecedorAtualizado);
}

// DELETE: api/forncedor/1
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    if (!_service.Delete(id))
        return NotFound();

    return NoContent();
}
}
