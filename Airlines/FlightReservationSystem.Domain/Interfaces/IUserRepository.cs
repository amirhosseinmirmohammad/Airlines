using FlightReservationsSystem.Domain.Entities;

namespace FlightReservationsSystem.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);

        Task<User> GetByIdAsync(string userId); 

        Task AddUserAsync(User user);
    }
}
