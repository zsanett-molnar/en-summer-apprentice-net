using System;
using System.Collections.Generic;

namespace TMS.Models;

public partial class TotalNumberOfTicketsPerCategory
{
    public int? TicketCategoryId { get; set; }

    public int? SumaBiletelorVandute { get; set; }

    public decimal? SumaVanzarilorTotalePerCategorie { get; set; }
}
