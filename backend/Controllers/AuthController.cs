using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Api.Models;

namespace Watchlist.Api.Controllers;

public record CredencialesDto(string Username, string Password);

[ApiController]
[Route("api/[controller]")]
public class AuthController(WatchlistContext db) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(CredencialesDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("Usuario y contraseña obligatorios.");

        if (await db.Usuarios.AnyAsync(u => u.Username == dto.Username))
            return Conflict("Ese usuario ya existe.");

        var usuario = new Usuario
        {
            Username = dto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();

        return Ok(new { usuario.Id, usuario.Username });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(CredencialesDto dto)
    {
        var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.Password))
            return Unauthorized("Usuario o contraseña incorrectos.");

        return Ok(new { usuario.Id, usuario.Username });
    }
}