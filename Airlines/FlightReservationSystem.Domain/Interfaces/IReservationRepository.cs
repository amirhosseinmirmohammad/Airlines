using FlightReservationsSystem.Domain.Entities;

namespace FlightReservationsSystem.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);

        Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId);

        Task<Reservation> GetByIdAsync(Guid reservationId);

        Task DeleteReservationByIdAsync(Reservation reservation);
    }
}
