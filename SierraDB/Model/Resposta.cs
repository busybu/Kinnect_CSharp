using System;
using System.Collections.Generic;

namespace SierraDB.Model;

public partial class Resposta
{
    public int Id { get; set; }

    public int Idaluno { get; set; }

    public int Idquestoes { get; set; }

    public string? Resposta1 { get; set; }

    public virtual Aluno? IdalunoNavigation { get; set; } = null!;

    public virtual Questo? IdquestoesNavigation { get; set; } = null!;
}
