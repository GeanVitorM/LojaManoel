using LojaManoel.API.Models;
using LojaManoel.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LojaManoel.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pedido>>> ObterTodos()
    {
        var pedidos = await _pedidoService.ObterTodos();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> ObterPorId(int id)
    {
        var pedido = await _pedidoService.ObterPorId(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> Criar([FromBody] Pedido pedido)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var novoPedido = await _pedidoService.Criar(pedido);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoPedido.Id }, novoPedido);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Pedido>> Atualizar(int id, [FromBody] Pedido pedido)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pedidoAtualizado = await _pedidoService.Atualizar(id, pedido);
        if (pedidoAtualizado == null) return NotFound();

        return Ok(pedidoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var resultado = await _pedidoService.Deletar(id);
        if (!resultado) return NotFound();

        return NoContent();
    }
}
