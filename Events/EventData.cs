namespace EasyTicket
{
    public class EventData
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string OrganizerName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal TicketPrice { get; set; }
        public int MaxGuests { get; set; }
        public int CurrentGuests { get; set; }
    }
}