using System;
using System.Collections.Generic;

namespace Watchlist.Api.Models;

public partial class ListaTitulo
{
    public int ListaId { get; set; }

    public int TituloId { get; set; }

    public DateTime? FechaAgregado { get; set; }

    public virtual Lista Lista { get; set; } = null!;

    public virtual Titulo Titulo { get; set; } = null!;
}
