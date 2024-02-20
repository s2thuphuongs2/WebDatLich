using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public DateTime? CanceledAt { get; set; }

    public string? Status { get; set; }

    public int? IdCanceler { get; set; }

    public int? IdProvider { get; set; }

    public int? IdCustomer { get; set; }

    public int? IdWork { get; set; }

    public int? IdInvoice { get; set; }

    public virtual ICollection<Exchange> ExchangeIdAppointmentRequestedNavigations { get; set; } = new List<Exchange>();

    public virtual ICollection<Exchange> ExchangeIdAppointmentRequestorNavigations { get; set; } = new List<Exchange>();

    public virtual User? IdCancelerNavigation { get; set; }

    public virtual User? IdCustomerNavigation { get; set; }

    public virtual Invoice? IdInvoiceNavigation { get; set; }

    public virtual User? IdProviderNavigation { get; set; }

    public virtual Work? IdWorkNavigation { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
