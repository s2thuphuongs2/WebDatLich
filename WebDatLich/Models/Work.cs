using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class Work
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Duration { get; set; }

    public decimal? Price { get; set; }

    public bool? Editable { get; set; }

    public string? Target { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
