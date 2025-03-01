using FlightReservationsSystem.Application.DTOs;
using FlightReservationsSystem.Application.Interfaces;
using FlightReservationsSystem.Domain.Entities;
using FlightReservationsSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightReservationsSystem.Application.Services
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository userRepository, UserManager<User> userManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(string fullName, string email, string password)
        {
            var user = new User(fullName, email, password);

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception("User registration failed.");

            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                throw new Exception("Invalid credentials.");

            return GenerateJwtToken(user);
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"],
                                             _configuration["JwtSettings:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryInMinutes"])),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            return new UserDto
            {
                FullName = user.FullName,
                Email = user.Email
            };
        }


        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
    }
}
