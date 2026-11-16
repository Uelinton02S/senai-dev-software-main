/*
using MinhaApi.Services;
using MinhaApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TipoController : ControllerBase
{
private readonly ITipoService _service;


public TipoController(ITipoService service)
{
    _service = service;
}

// GET: api/tipo
[HttpGet]
public IActionResult GetAll()
{
    return Ok(_service.GetAll());
}

// GET: api/tipo/1
[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var tipo = _service.GetById(id);

    if (tipo == null)
        return NotFound();

    return Ok(tipo);
}

// POST: api/tipo
[HttpPost]
public IActionResult Create(Tipo tipo)
{
    var novoTipo = _service.Create(tipo);

    return CreatedAtAction(
        nameof(GetById),
        new { id = novoTipo.Id },
        novoTipo
    );
}

// PUT: api/tipo/1
[HttpPut("{id}")]
public IActionResult Update(int id, Tipo tipo)
{
    var tipoAtualizado = _service.Update(id, tipo);

    if (tipoAtualizado == null)
        return NotFound();

    return Ok(tipoAtualizado);
}

// DELETE: api/tipo/1
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    if (!_service.Delete(id))
        return NotFound();

    return NoContent();
}


}
*/
