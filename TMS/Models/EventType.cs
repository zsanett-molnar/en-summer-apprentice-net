using System;
using System.Collections.Generic;

namespace TMS.Models;

public partial class EventType
{
    public int EventTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
