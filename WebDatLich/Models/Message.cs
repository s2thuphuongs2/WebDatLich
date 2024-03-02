using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class Message
{
    public int Id { get; set; }
    [DisplayName("Nhắn vào lúc")]
    public DateTime? CreatedAt { get; set; }
    [DisplayName("Lời nhắn")]
    public string? Message1 { get; set; }

    public int? IdAuthor { get; set; }

    public int? IdAppointment { get; set; }

    public virtual Appointment? IdAppointmentNavigation { get; set; }

    public virtual User? IdAuthorNavigation { get; set; }
}
