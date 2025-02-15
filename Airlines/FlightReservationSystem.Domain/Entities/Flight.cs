namespace FlightReservationSystem.Domain.Entities
{
    public class Flight
    {
        public Flight(string flightNumber, string origin, string destination, DateTime departureTime, DateTime arrivalTime, int availableSeats)
        {
            Id = Guid.NewGuid();
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            AvailableSeats = availableSeats;
            Reservations = new List<Reservation>();
        }

        public Guid Id { get; set; }

        public string FlightNumber { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int AvailableSeats { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public bool ReserveSeat(int seats = 1)
        {
            if (AvailableSeats >= seats)
            {
                AvailableSeats -= seats;
                return true;
            }
            return false;
        }
    }
}
