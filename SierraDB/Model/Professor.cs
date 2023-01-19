using System;
using System.Collections.Generic;

namespace SierraDB.Model;

public partial class Professor
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Senha { get; set; }

    public virtual ICollection<Modulo> Modulos { get; } = new List<Modulo>();
}
