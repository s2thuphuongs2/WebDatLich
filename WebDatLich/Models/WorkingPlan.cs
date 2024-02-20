using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class WorkingPlan
{
    public int IdProvider { get; set; }

    public string? Monday { get; set; }

    public string? Tuesday { get; set; }

    public string? Wednesday { get; set; }

    public string? Thursday { get; set; }

    public string? Friday { get; set; }

    public string? Saturday { get; set; }

    public string? Sunday { get; set; }

    public virtual User IdProviderNavigation { get; set; } = null!;
}
