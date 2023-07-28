using System;
using System.Collections.Generic;

namespace TMS.Models;

public partial class Customer
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
