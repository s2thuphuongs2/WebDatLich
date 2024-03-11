using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class Appointment
{
    public int Id { get; set; }

    [DisplayName("Bắt đầu")]
    public DateTime? Start { get; set; }

    [DisplayName("Kết thúc")]
    public DateTime? End { get; set; }

    [DisplayName("Hủy vào lúc")]
    public DateTime? CanceledAt { get; set; }

    [DisplayName("Trạng thái")]
    public string? Status { get; set; }

    public int? IdCanceler { get; set; }

    public int? IdProvider { get; set; }

    public int? IdCustomer { get; set; }

    public int? IdWork { get; set; }

    public int? IdInvoice { get; set; }
    [DisplayName("Yêu cầu đổi lịch")]
    public virtual ICollection<Exchange> ExchangeIdAppointmentRequestedNavigations { get; set; } = new List<Exchange>();
    [DisplayName("Người yêu cầu đổi lịch")]
    public virtual ICollection<Exchange> ExchangeIdAppointmentRequestorNavigations { get; set; } = new List<Exchange>();

    public virtual User? IdCancelerNavigation { get; set; }
    [DisplayName("Người đặt lịch")]
    public virtual User? IdCustomerNavigation { get; set; }

    public virtual Invoice? IdInvoiceNavigation { get; set; }
    [DisplayName("Người tạo lịch")]
    public virtual User? IdProviderNavigation { get; set; }

    public virtual Work? IdWorkNavigation { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
