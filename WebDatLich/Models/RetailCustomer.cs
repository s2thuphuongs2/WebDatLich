using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class RetailCustomer
{
    public int IdCustomer { get; set; }

    public virtual User IdCustomerNavigation { get; set; } = null!;
}
