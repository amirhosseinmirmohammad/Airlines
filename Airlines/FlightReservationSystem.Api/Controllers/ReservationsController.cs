using FlightReservationsSystem.Application.DTOs;
using FlightReservationsSystem.Application.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationSystem.Api.Controllers
{
    /// <summary>
    /// Controller for managing reservation-related operations (create, get, cancel).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ReservationsController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the ReservationController class.
        /// </summary>
        /// <param name="reservationService">The reservation service.</param>
        public ReservationsController(IReservationService reservationService)
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
            var userId = HttpContext.User.Identity.GetUserId(); 
            var reservation = await _reservationService.CreateReservationAsync(flightId, userId);
            return CreatedAtAction(nameof(GetUserReservations), new { userId }, reservation);
        }

        /// <summary>
        /// Gets all reservations for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to get reservations for.</param>
        /// <returns>A list of reservations for the user.</returns>
        [HttpGet("myReservation")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetUserReservations()
        {
            var userId = HttpContext.User.Identity.GetUserId();
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
            await _reservationService.CancelReservationAsync(reservationId);
            return Ok();
        }


        private readonly IReservationService _reservationService;
    }
}
