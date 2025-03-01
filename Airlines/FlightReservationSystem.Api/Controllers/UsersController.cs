using FlightReservationsSystem.Application.DTOs;
using FlightReservationsSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationSystem.Api.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations (Register, Login).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registers a new user and returns a JWT token.
        /// </summary>
        /// <param name="userDto">The user details including full name, email, and password.</param>
        /// <returns>A JWT token if registration is successful.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var token = await _userService.RegisterAsync(userDto.FullName, userDto.Email, userDto.Password);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        /// <param name="userDto">The user credentials (email and password).</param>
        /// <returns>A JWT token if login is successful.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var token = await _userService.LoginAsync(userDto.Email, userDto.Password);
            return Ok(new { Token = token });
        }


        private readonly IUserService _userService;
    }
}
