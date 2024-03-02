using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class WorkingPlan
{
    public int IdProvider { get; set; }
    [DisplayName("Thứ 2")]
    public string? Monday { get; set; }
    [DisplayName("Thứ 3")]
    public string? Tuesday { get; set; }
    [DisplayName("Thứ 4")]
    public string? Wednesday { get; set; }
    [DisplayName("Thứ 5")]
    public string? Thursday { get; set; }
    [DisplayName("Thứ 6")]
    public string? Friday { get; set; }
    [DisplayName("Thứ 7")]
    public string? Saturday { get; set; }
    [DisplayName("Chủ nhật")]
    public string? Sunday { get; set; }

    public virtual User IdProviderNavigation { get; set; } = null!;
}
