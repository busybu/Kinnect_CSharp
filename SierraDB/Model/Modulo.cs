using System;
using System.Collections.Generic;

namespace SierraDB.Model;

public partial class Modulo
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public int Idprofessor { get; set; }

    public virtual Professor? IdprofessorNavigation { get; set; } = null!;

    public virtual ICollection<Questo> Questos { get; } = new List<Questo>();
}
