namespace FlightReservationSystem.Application.DTOs
{
    public class ReservationDto
    {
        public Guid Id { get; set; }

        public Guid FlightId { get; set; }

        public Guid UserId { get; set; }

        public DateTime ReservationDate { get; set; }
    }
}
