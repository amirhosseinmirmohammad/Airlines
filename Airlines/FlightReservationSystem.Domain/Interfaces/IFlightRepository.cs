using FlightReservationSystem.Domain.Entities;

namespace FlightReservationSystem.Domain.Interfaces
{
    public interface IFlightRepository
    {
        Task<Flight> GetByIdAsync(Guid id);

        Task<IEnumerable<Flight>> SearchAsync(string origin, string destination, DateTime? departureDate);

        Task UpdateAsync(Flight flight);
    }
}
