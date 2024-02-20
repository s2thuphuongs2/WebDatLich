using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class Provider
{
    public int IdProvider { get; set; }

    public virtual User IdProviderNavigation { get; set; } = null!;
}
