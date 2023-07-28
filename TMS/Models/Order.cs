using System;
using System.Collections.Generic;

namespace TMS.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? TicketCategoryId { get; set; }

    public DateTime? OrderedAt { get; set; }

    public int? NumberOfTickets { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual TicketCategory? TicketCategory { get; set; }

    public virtual Customer? User { get; set; }
}
