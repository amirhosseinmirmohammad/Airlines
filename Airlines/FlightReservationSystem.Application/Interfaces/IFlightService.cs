using FlightReservationSystem.Application.DTOs;

namespace FlightReservationSystem.Application.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> SearchFlightsAsync(string origin, string destination, DateTime? departureDate);

        Task<FlightDto> GetFlightByIdAsync(Guid flightId);
    }
}
