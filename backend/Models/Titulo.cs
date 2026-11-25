using System;
using System.Collections.Generic;

namespace Watchlist.Api.Models;

public partial class Titulo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int TipoId { get; set; }

    public short AnioEstreno { get; set; }

    public string? Sinopsis { get; set; }

    public string? ImagenUrl { get; set; }

    public virtual ICollection<ListaTitulo> ListaTitulos { get; set; } = new List<ListaTitulo>();

    public virtual Tipo Tipo { get; set; } = null!;

    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();
}
