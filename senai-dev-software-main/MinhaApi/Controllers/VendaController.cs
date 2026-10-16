using MinhaApi.DTO;
using MinhaApi.Models;
using MinhaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers
{
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
               
            });

            return Ok(vendaResponses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var venda = _vendaService.GetById(id);

            if (venda == null)
            {
                return NotFound();
            }

            var vendaResponse = new VendaResponse
            {
                idproduto = venda.idprodutos,
                idcliente = venda.idclientes,
                quantidade = venda.quantidade,
             
            };

            return Ok(vendaResponse);
        }

        [HttpPost]
        public IActionResult Create([FromBody] VendaRequest request) 
        {
            
            var novaVenda = new Venda
            {
                idprodutos = request.idproduto,
                idclientes = request.idcliente,
                quantidade = request.quantidade,
                
            };

            var vendaCriada = _vendaService.Create(novaVenda);

            var vendaResponse = new VendaResponse
            {
                idproduto = vendaCriada.idprodutos,
                idcliente = vendaCriada.idclientes,
                quantidade = vendaCriada.quantidade,
         
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = vendaCriada.id },
                vendaResponse
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VendaRequest request)
        {
            
            var dadosAtualizados = new Venda
            {
                idprodutos = request.idproduto,
                idclientes = request.idcliente,
                quantidade = request.quantidade,
           
            };

            var vendaAtualizada = _vendaService.Update(id, dadosAtualizados);

            if (vendaAtualizada == null)
            {
                return NotFound();
            }

            var vendaResponse = new VendaResponse
            {
                idproduto = vendaAtualizada.idprodutos,
                idcliente = vendaAtualizada.idclientes,
                quantidade = vendaAtualizada.quantidade,
                data_venda = vendaAtualizada.data_venda
            };

            return Ok(vendaResponse);
        }
    }
}
