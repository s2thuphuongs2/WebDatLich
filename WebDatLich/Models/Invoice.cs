using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class Invoice
{
    public int Id { get; set; }
    [DisplayName("Mã hóa đơn")]
    public string? Number { get; set; }
    [DisplayName("Trạng thái")]
    public string? Status { get; set; }
    [DisplayName("Tổng tiền")]
    public decimal? TotalAmount { get; set; }
    [DisplayName("Ngày xuất")]
    public DateTime? Issued { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
