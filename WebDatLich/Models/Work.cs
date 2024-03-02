using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class Work
{
    public int Id { get; set; }
    [DisplayName("Nội dung")]
    public string? Name { get; set; }
    [DisplayName("Thời lượng")]
    public int? Duration { get; set; }
    [DisplayName("Chi phí")]
    public decimal? Price { get; set; }
    [DisplayName("Thay đổi")]
    public bool? Editable { get; set; }
    [DisplayName("Đối tượng")]
    public string? Target { get; set; }
    [DisplayName("Mô tả")]
    public string? Description { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
