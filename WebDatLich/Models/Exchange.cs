using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class Exchange
{
    public int Id { get; set; }

    public string? ExchangeStatus { get; set; }

    public int? IdAppointmentRequestor { get; set; }

    public int? IdAppointmentRequested { get; set; }

    public virtual Appointment? IdAppointmentRequestedNavigation { get; set; }

    public virtual Appointment? IdAppointmentRequestorNavigation { get; set; }
}
