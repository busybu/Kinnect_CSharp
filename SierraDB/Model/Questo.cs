using System;
using System.Collections.Generic;

namespace SierraDB.Model;

public partial class Questo
{
    public int Id { get; set; }

    public string? Descricao { get; set; }

    public int Idmodulo { get; set; }

    public int? Peso { get; set; }

    public string? Resposta { get; set; }

    public virtual Modulo? IdmoduloNavigation { get; set; } = null!;

    public virtual ICollection<Resposta> RespostaNavigation { get; } = new List<Resposta>();
}
