using FlightReservationSystem.Domain.Entities;
using FlightReservationSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FlightReservationSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task AddUserAsync(User user)
        {
            await _userManager.CreateAsync(user);
        }


        private readonly UserManager<User> _userManager;
    }
}
