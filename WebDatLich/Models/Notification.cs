using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class Notification
{
    public int Id { get; set; }

    [DisplayName("Tiêu đề")]
    public string? Title { get; set; }

    [DisplayName("Lời nhắn")]
    public string? Message { get; set; }

    [DisplayName("Vào lúc")]
    public DateTime? CreatedAt { get; set; }

    public string? Url { get; set; }

    [DisplayName("Đã xem")]
    public bool? IsRead { get; set; }

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
