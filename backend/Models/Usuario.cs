using System;
using System.Collections.Generic;

namespace Watchlist.Api.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Lista> Lista { get; set; } = new List<Lista>();
}
