using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VendaController : ControllerBase {
    private readonly IVendaService _vendaService;

    public VendaController(IVendaService vendaService) {
        _vendaService = vendaService;
    }

    [HttpGet]
    public IActionResult GetAll() {
        var vendas = _vendaService.GetAll();
        return Ok(vendas);
    }

    [HttpPost]
    public IActionResult Add(Venda venda) {
        _vendaService.Add(venda);
        return CreatedAtAction(nameof(GetAll), new { id = venda.id }, venda);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Venda venda) {
        if (id != venda.id) {
            return BadRequest();
        }
        _vendaService.Update(venda);
        return NoContent();
    }

}