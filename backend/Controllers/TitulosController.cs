using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Api.Models;

namespace Watchlist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TitulosController(WatchlistContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        string? nombre, string? tipo, string? genero, int? anio)
    {
        var query = db.Titulos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nombre))
            query = query.Where(t => t.Nombre.Contains(nombre));

        if (!string.IsNullOrWhiteSpace(tipo))
            query = query.Where(t => t.Tipo.Nombre == tipo);

        if (!string.IsNullOrWhiteSpace(genero))
            query = query.Where(t => t.Generos.Any(g => g.Nombre == genero));

        if (anio.HasValue)
            query = query.Where(t => t.AnioEstreno == anio.Value);

        var titulos = await query
            .OrderBy(t => t.Nombre)
            .Select(t => new
            {
                t.Id,
                t.Nombre,
                Tipo = t.Tipo.Nombre,
                t.AnioEstreno,
                t.ImagenUrl
            })
            .ToListAsync();

        return Ok(titulos);
    }
    
        [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var titulo = await db.Titulos
            .Where(t => t.Id == id)
            .Select(t => new
            {
                t.Id,
                t.Nombre,
                Tipo = t.Tipo.Nombre,
                t.AnioEstreno,
                t.Sinopsis,
                t.ImagenUrl,
                Generos = t.Generos.Select(g => g.Nombre)
            })
            .FirstOrDefaultAsync();

        if (titulo is null)
            return NotFound();

        return Ok(titulo);
    }
}