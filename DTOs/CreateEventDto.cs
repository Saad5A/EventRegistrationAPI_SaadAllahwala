namespace EventRegistrationAPI_SaadAllahwala.DTOs
{
    public class CreateEventDto
    {
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
    }
}
