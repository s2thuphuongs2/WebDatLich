using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public string? Status { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? Issued { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
