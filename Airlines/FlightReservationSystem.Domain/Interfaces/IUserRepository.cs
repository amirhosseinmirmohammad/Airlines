using FlightReservationSystem.Domain.Entities;

namespace FlightReservationSystem.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);
    }
}
