namespace TMS.Models.Dto
{
    public class EventPostDto
    {
        public int EventId { get; set; }

        public int? LocationId { get; set; }

        public int? EventTypeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
