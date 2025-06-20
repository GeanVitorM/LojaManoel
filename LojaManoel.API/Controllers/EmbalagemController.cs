﻿using LojaManoel.API.DTOs;
using LojaManoel.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LojaManoel.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmbalagemController : ControllerBase
{
    private readonly IEmbalagemService _embalagemService;

    public EmbalagemController(IEmbalagemService embalagemService)
    {
        _embalagemService = embalagemService;
    }
    
    [HttpPost("processar")]
    [ProducesResponseType(typeof(EmbalagemOutputDto), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<EmbalagemOutputDto>> ProcessarEmbalagem([FromBody] PedidoInputDto input)
    {
        if (input?.Pedidos == null || !input.Pedidos.Any())
        {
            return BadRequest("Lista de pedidos não pode estar vazia");
        }

        var resultado = await _embalagemService.ProcessarEmbalagem(input);
        return Ok(resultado);
    }
}
