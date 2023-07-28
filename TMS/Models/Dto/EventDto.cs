namespace TMS.Models.Dto
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public int? EventTypeId { get; set; }
        public int? LocationId { get; set; }

    }
}
