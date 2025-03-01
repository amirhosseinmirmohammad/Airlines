namespace FlightReservationsSystem.Application.DTOs
{
    public class ReservationDto
    {
        public Guid Id { get; set; }

        public Guid FlightId { get; set; }

        public string UserId { get; set; }

        public DateTime ReservationDate { get; set; }
    }
}
