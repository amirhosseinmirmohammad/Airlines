namespace FlightReservationSystem.Domain.Entities
{
    public class Reservation
    {
        public Reservation(Guid flightId, Guid userId)
        {
            Id = Guid.NewGuid();
            FlightId = flightId;
            UserId = userId;
            ReservationDate = DateTime.UtcNow;
        }

        public Guid Id { get; set; }

        public Guid FlightId { get; set; }

        public Guid UserId { get; set; }

        public DateTime ReservationDate { get; set; }

        public Flight Flight { get; set; }

        public User User { get; set; }
    }
}
