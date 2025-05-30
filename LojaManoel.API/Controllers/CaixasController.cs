using LojaManoel.API.Models;
using LojaManoel.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LojaManoel.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CaixasController : ControllerBase
{
    private readonly ICaixaService _caixaService;

    public CaixasController(ICaixaService caixaService)
    {
        _caixaService = caixaService;
    }

    [HttpGet]
    public ActionResult<List<Caixa>> ObterCaixasDisponiveis()
    {
        var caixas = _caixaService.ObterCaixasDisponiveis();
        return Ok(caixas);
    }
}
