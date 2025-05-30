using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LojaManoel.IdentityServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new IdentityUser { UserName = dto.UserName, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (result.Succeeded)
            return Ok();
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null) return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded) return Unauthorized();

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("segredo_super_forte_1234567890"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: new[] { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.NameIdentifier, user.Id) },
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );
        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateDto dto)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null) return NotFound();

        user.Email = dto.Email ?? user.Email;
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
            return Ok();
        return BadRequest(result.Errors);
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null) return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
            return Ok();
        return BadRequest(result.Errors);
    }
}

public record RegisterDto(string UserName, string Email, string Password);
public record LoginDto(string UserName, string Password);
public record UpdateDto(string? Email);
