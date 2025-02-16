using FlightReservationSystem.Application.DTOs;
using FlightReservationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationSystem.Api.Controllers
{
    /// <summary>
    /// Controller for managing reservation-related operations (create, get, cancel).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication for all actions in this controller
    public class ReservationController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the ReservationController class.
        /// </summary>
        /// <param name="reservationService">The reservation service.</param>
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Creates a new reservation for a flight.
        /// </summary>
        /// <param name="flightId">The ID of the flight to be reserved.</param>
        /// <returns>Reservation details.</returns>
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> Create([FromBody] Guid flightId)
        {
            var userId = HttpContext.User.Identity.Name;  // Get the current user's ID
            var reservation = await _reservationService.CreateReservationAsync(flightId, userId);
            return CreatedAtAction(nameof(GetUserReservations), new { userId = userId }, reservation);
        }

        /// <summary>
        /// Gets all reservations for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to get reservations for.</param>
        /// <returns>A list of reservations for the user.</returns>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetUserReservations(string userId)
        {
            var reservations = await _reservationService.GetUserReservationsAsync(userId);
            return Ok(reservations);
        }

        /// <summary>
        /// Cancels an existing reservation.
        /// </summary>
        /// <param name="reservationId">The ID of the reservation to cancel.</param>
        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> Cancel(Guid reservationId)
        {
            try
            {
                await _reservationService.CancelReservationAsync(reservationId);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });  
            }
        }


        private readonly IReservationService _reservationService;
    }
}
