using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class Role
{
    public int Id { get; set; }
    [DisplayName("Quyền")]
    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
