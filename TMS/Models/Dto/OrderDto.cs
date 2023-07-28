namespace TMS.Models.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime? OrderedAt { get; set; }
        public int? NumberOfTickets { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
