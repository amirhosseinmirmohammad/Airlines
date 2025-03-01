using FlightReservationsSystem.Domain.Entities;
using FlightReservationsSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FlightReservationsSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task AddUserAsync(User user)
        {
            await _userManager.CreateAsync(user);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }


        private readonly UserManager<User> _userManager;
    }
}
