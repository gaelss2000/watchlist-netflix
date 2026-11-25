using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Api.Models;

namespace Watchlist.Api.Controllers;

public record NuevaListaDto(int UsuarioId, string Nombre);

[ApiController]
[Route("api/[controller]")]
public class ListasController(WatchlistContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByUsuario(int usuarioId)
    {
        var listas = await db.Listas
            .Where(l => l.UsuarioId == usuarioId)
            .OrderBy(l => l.Nombre)
            .Select(l => new
            {
                l.Id,
                l.Nombre,
                TotalTitulos = l.ListaTitulos.Count
            })
            .ToListAsync();

        return Ok(listas);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NuevaListaDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            return BadRequest("El nombre de la lista es obligatorio.");

        if (!await db.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId))
            return NotFound("El usuario no existe.");

        var lista = new Lista { UsuarioId = dto.UsuarioId, Nombre = dto.Nombre };
        db.Listas.Add(lista);
        await db.SaveChangesAsync();

        return Ok(new { lista.Id, lista.Nombre, TotalTitulos = 0 });
    }
    
    [HttpGet("{id}/titulos")]
    public async Task<IActionResult> GetTitulos(int id)
    {
        if (!await db.Listas.AnyAsync(l => l.Id == id))
            return NotFound("La lista no existe.");

        var titulos = await db.ListaTitulos
            .Where(lt => lt.ListaId == id)
            .OrderByDescending(lt => lt.FechaAgregado)
            .Select(lt => new
            {
                lt.Titulo.Id,
                lt.Titulo.Nombre,
                Tipo = lt.Titulo.Tipo.Nombre,
                lt.Titulo.AnioEstreno,
                lt.Titulo.ImagenUrl
            })
            .ToListAsync();

        return Ok(titulos);
    }

    [HttpPost("{id}/titulos/{tituloId}")]
    public async Task<IActionResult> AddTitulo(int id, int tituloId)
    {
        if (!await db.Listas.AnyAsync(l => l.Id == id))
            return NotFound("La lista no existe.");

        if (!await db.Titulos.AnyAsync(t => t.Id == tituloId))
            return NotFound("El título no existe.");

        if (await db.ListaTitulos.AnyAsync(lt => lt.ListaId == id && lt.TituloId == tituloId))
            return Conflict("El título ya está en la lista.");

        db.ListaTitulos.Add(new ListaTitulo { ListaId = id, TituloId = tituloId });
        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}/titulos/{tituloId}")]
    public async Task<IActionResult> RemoveTitulo(int id, int tituloId)
    {
        var relacion = await db.ListaTitulos
            .FirstOrDefaultAsync(lt => lt.ListaId == id && lt.TituloId == tituloId);

        if (relacion is null)
            return NotFound("Ese título no está en la lista.");

        db.ListaTitulos.Remove(relacion);
        await db.SaveChangesAsync();

        return NoContent();
    }
}