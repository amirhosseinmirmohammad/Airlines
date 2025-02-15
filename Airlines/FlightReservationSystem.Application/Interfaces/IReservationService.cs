using FlightReservationSystem.Application.DTOs;

namespace FlightReservationSystem.Application.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDto> CreateReservationAsync(Guid flightId, Guid userId);

        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(Guid userId);

        Task CancelReservationAsync(Guid reservationId);

        Task<ReservationDto> GetReservationByIdAsync(Guid reservationId);

        Task<ReservationDto> UpdateReservationAsync(Guid reservationId, DateTime newDepartureDate, int newSeats);
    }
}
