using FlightReservationSystem.Application.DTOs;

namespace FlightReservationSystem.Application.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllAsync();

        Task<IEnumerable<FlightDto>> SearchAsync(string origin, string destination, DateTime? departureDate);

        Task<FlightDto> GetByIdAsync(Guid flightId);
    }
}
