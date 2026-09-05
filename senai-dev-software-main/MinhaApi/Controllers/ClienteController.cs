using MinhaApi.Services;
using MinhaApi.Models;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Cliente> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Cliente> GetById(int id)
    {
        var cliente = _service.GetById(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public ActionResult<Cliente> Create(Cliente cliente)
    {
        var createdCliente = _service.Create(cliente);
        return CreatedAtAction(nameof(GetById), new { id = createdCliente.Id }, createdCliente);
    }

    [HttpPut("{id}")]
    public ActionResult<Cliente> Update(int id, Cliente cliente)
    {
        var updatedCliente = _service.Update(id, cliente);
        if (updatedCliente == null) return NotFound();
        return Ok(updatedCliente);
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete(int id)
    {
        var result = _service.Delete(id);
        if (!result) return NotFound();
        return Ok(result);
    }
}