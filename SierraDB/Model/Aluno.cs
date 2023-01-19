using System;
using System.Collections.Generic;

namespace SierraDB.Model;

public partial class Aluno
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Senha { get; set; }

    public virtual ICollection<Resposta> Resposta { get; } = new List<Resposta>();
}
