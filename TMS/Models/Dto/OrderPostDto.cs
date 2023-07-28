namespace TMS.Models.Dto
{
    public class OrderPostDto
    {
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public int? TicketCategoryId { get; set; }

        public int? NumberOfTickets { get; set; }

    }
}
