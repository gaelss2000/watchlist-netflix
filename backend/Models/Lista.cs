using System;
using System.Collections.Generic;

namespace Watchlist.Api.Models;

public partial class Lista
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ListaTitulo> ListaTitulos { get; set; } = new List<ListaTitulo>();

    public virtual Usuario Usuario { get; set; } = null!;
}
