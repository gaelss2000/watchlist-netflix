using System;
using System.Collections.Generic;

namespace Watchlist.Api.Models;

public partial class Genero
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Titulo> Titulos { get; set; } = new List<Titulo>();
}
