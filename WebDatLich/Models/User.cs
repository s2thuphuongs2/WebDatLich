using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class User
{
    public int Id { get; set; }

    [DisplayName("Tên người dùng")]
    public string Username { get; set; } = null!;

    [DisplayName("Mật khẩu")]
    public string Password { get; set; } = null!;

    [DisplayName("Tên")]
    public string? FirstName { get; set; }

    [DisplayName("Họ và tên lót")]
    public string? LastName { get; set; }

    public string? Email { get; set; }

    [DisplayName("Số điện thoại")]
    public string? Mobile { get; set; }

    [DisplayName("Địa chỉ")]
    public string? Street { get; set; }

    [DisplayName("Thành phố")]
    public string? City { get; set; }

    public virtual ICollection<Appointment> AppointmentIdCancelerNavigations { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentIdCustomerNavigations { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentIdProviderNavigations { get; set; } = new List<Appointment>();

    public virtual CorporateCustomer? CorporateCustomer { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Provider? Provider { get; set; }

    public virtual RetailCustomer? RetailCustomer { get; set; }

    public virtual WorkingPlan? WorkingPlan { get; set; }

    public virtual ICollection<Work> IdWorks { get; set; } = new List<Work>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
