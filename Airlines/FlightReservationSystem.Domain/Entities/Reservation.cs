namespace FlightReservationsSystem.Domain.Entities
{
    public class Reservation
    {
        public Reservation(Guid flightId, string userId)
        {
            Id = Guid.NewGuid(); 
            FlightId = flightId;
            UserId = userId;
            ReservationDate = DateTime.UtcNow;
        }

        public Guid Id { get; set; } 

        public Guid FlightId { get; set; }

        public string UserId { get; set; } 

        public DateTime ReservationDate { get; set; }

        public virtual Flight Flight { get; set; }

        public virtual User User { get; set; }
    }
}