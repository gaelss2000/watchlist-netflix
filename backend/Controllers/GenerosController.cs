using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Api.Models;

namespace Watchlist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenerosController(WatchlistContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var generos = await db.Generos
            .OrderBy(g => g.Nombre)
            .Select(g => g.Nombre)
            .ToListAsync();

        return Ok(generos);
    }
}