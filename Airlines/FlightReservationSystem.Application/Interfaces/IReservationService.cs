using FlightReservationsSystem.Application.DTOs;

namespace FlightReservationsSystem.Application.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDto> CreateReservationAsync(Guid flightId, string userId);

        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(string userId);

        Task CancelReservationAsync(Guid reservationId);

        Task<ReservationDto> GetReservationByIdAsync(Guid reservationId);

        Task<ReservationDto> UpdateReservationAsync(Guid reservationId, DateTime newDepartureDate, int newSeats);
    }
}
