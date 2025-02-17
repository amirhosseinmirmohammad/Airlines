using FlightReservationSystem.Application.DTOs;

namespace FlightReservationSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(string fullName, string email, string password);

        Task<string> LoginAsync(string email, string password);

        Task LogoutAsync();

        Task<UserDto> GetByIdAsync(string userId);
    }
}
