
using MinhaApi.DTO;
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
        var vendaResponses = vendas.Select(v => new VendaResponse
        {
            idproduto = v.idprodutos,
            idcliente = v.idclientes,
            quantidade = v.quantidade,
            data_venda = v.data_venda
        });
        return Ok(vendaResponses);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var venda = _vendaService.GetById(id);
        var vendaResponse = venda != null ? new VendaResponse
        {
            
            idproduto = venda.idprodutos,
            idcliente = venda.idclientes,
            quantidade = venda.quantidade,
          
            data_venda = venda.data_venda
        } : null;

        if (venda == null)
        {
            return NotFound();
        }

        return Ok(vendaResponse);
    }

    [HttpPost]
    public IActionResult Add(Venda venda)
    {
        var novaVenda = _vendaService.Create(venda);
        var vendaResponse = new VendaResponse
        {
          
            idproduto = novaVenda.idprodutos,
            idcliente = novaVenda.idclientes,
            quantidade = novaVenda.quantidade,
        
            data_venda = novaVenda.data_venda
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = novaVenda.id },
            vendaResponse
        );
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Venda venda)
    {
        var vendaAtualizada = _vendaService.Update(id, venda);
        var vendaResponse = vendaAtualizada != null ? new VendaResponse
        {
            
            idproduto = vendaAtualizada.idprodutos,
            idcliente = vendaAtualizada.idclientes,
            quantidade = vendaAtualizada.quantidade,
    
            data_venda = vendaAtualizada.data_venda
        } : null;

        if (vendaAtualizada == null)
        {
            return NotFound();
        }

        return Ok(vendaResponse);
    }
}

