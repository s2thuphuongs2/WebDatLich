using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class Message
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Message1 { get; set; }

    public int? IdAuthor { get; set; }

    public int? IdAppointment { get; set; }

    public virtual Appointment? IdAppointmentNavigation { get; set; }

    public virtual User? IdAuthorNavigation { get; set; }
}
