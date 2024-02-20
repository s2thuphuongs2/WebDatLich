using System;
using System.Collections.Generic;

namespace WebDatLich.Models;

public partial class CorporateCustomer
{
    public int IdCustomer { get; set; }

    public string? VatNumber { get; set; }

    public string? CompanyName { get; set; }

    public virtual User IdCustomerNavigation { get; set; } = null!;
}
