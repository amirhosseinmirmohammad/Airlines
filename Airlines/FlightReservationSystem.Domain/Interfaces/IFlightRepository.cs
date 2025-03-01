using FlightReservationsSystem.Domain.Entities;

namespace FlightReservationsSystem.Domain.Interfaces
{
    public interface IFlightRepository
    {
        Task<Flight> GetByIdAsync(Guid id);

        Task<IEnumerable<Flight>> SearchAsync(string origin, string destination, DateTime? departureDate);

        Task<IEnumerable<Flight>> GetAllAsync();

        Task UpdateAsync(Flight flight);
    }
}
