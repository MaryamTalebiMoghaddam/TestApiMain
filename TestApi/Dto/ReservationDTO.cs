namespace TestApi.Dto
{
    public class ReservationDTO
    {
        public string? Name { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; } = string.Empty;
    }
}
