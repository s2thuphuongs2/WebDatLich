using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDatLich.Models;

public partial class CorporateCustomer
{
    public int IdCustomer { get; set; }

    [DisplayName("Số Vat")]
    public string? VatNumber { get; set; }

    [DisplayName("Tên công ty")]
    public string? CompanyName { get; set; }

    public virtual User IdCustomerNavigation { get; set; } = null!;
}
