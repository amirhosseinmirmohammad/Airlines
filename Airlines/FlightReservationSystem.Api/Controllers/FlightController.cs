using FlightReservationSystem.Application.DTOs;
using FlightReservationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationSystem.Api.Controllers
{
    /// <summary>
    /// Controller for managing flight-related operations (search flights).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication for all actions in this controller
    public class FlightController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the FlightController class.
        /// </summary>
        /// <param name="flightService">The flight service.</param>
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        /// <summary>
        /// Searches for flights based on the given origin, destination, and optional departure date.
        /// </summary>
        /// <param name="origin">The origin airport.</param>
        /// <param name="destination">The destination airport.</param>
        /// <param name="departureDate">Optional departure date filter.</param>
        /// <returns>A list of flights matching the search criteria.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FlightDto>>> Search([FromQuery] string origin, [FromQuery] string destination, [FromQuery] DateTime? departureDate)
        {
            var flights = await _flightService.SearchFlightsAsync(origin, destination, departureDate);
            return Ok(flights);
        }


        private readonly IFlightService _flightService;
    }
}
