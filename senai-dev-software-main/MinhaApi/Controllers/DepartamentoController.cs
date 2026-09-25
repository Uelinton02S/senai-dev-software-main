using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
private readonly IDepartamentoService _service;


public DepartamentoController(IDepartamentoService service)
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
    var departamento = _service.GetById(id);

    if (departamento == null)
        return NotFound();

    return Ok(departamento);
}

// POST: api/forncedor
[HttpPost]
public IActionResult Add([FromBody] Departamento departamento)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var novoDepartamento = _service.Add(departamento);

    return CreatedAtAction(
        nameof(GetById),
        new { id = novoDepartamento.Id },
        novoDepartamento
    );
}

// PUT: api/forncedor/1
[HttpPut("{id}")]
public IActionResult Update(int id, [FromBody] Departamento departamento)
{
    var departamentoAtualizado = _service.Update(id, departamento);

    if (departamentoAtualizado == null)
        return NotFound();

    return Ok(departamentoAtualizado);
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
