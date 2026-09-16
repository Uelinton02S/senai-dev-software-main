
using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _vendaService;

    public VendaController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var vendas = _vendaService.GetAll();
        return Ok(vendas);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var venda = _vendaService.GetById(id);

        if (venda == null)
        {
            return NotFound();
        }

        return Ok(venda);
    }

    [HttpPost]
    public IActionResult Add(Venda venda)
    {
        var novaVenda = _vendaService.Create(venda);

        return CreatedAtAction(
            nameof(GetById),
            new { id = novaVenda.id },
            novaVenda
        );
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Venda venda)
    {
        var vendaAtualizada = _vendaService.Update(id, venda);

        if (vendaAtualizada == null)
        {
            return NotFound();
        }

        return Ok(vendaAtualizada);
    }
}

