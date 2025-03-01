using FlightReservationsSystem.Application.DTOs;
using FlightReservationsSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationSystem.Api.Controllers
{
    /// <summary>
    /// Controller for managing flight-related operations (search flights).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FlightsController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the FlightController class.
        /// </summary>
        /// <param name="flightService">The flight service.</param>
        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        /// <summary>
        /// Get All flights.
        /// </summary>
        /// <returns>A list of all flights.</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<FlightDto>>> GetAll()
        {
            var flights = await _flightService.GetAllAsync();
            return Ok(flights);
        }

        /// <summary>
        /// Searches for flights based on the given origin, destination, and optional departure date.
        /// </summary>
        /// <param name="origin">The origin airport.</param>
        /// <param name="destination">The destination airport.</param>
        /// <param name="departureDate">Optional departure date filter.</param>
        /// <returns>A list of flights matching the search criteria.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FlightDto>>> Search([FromQuery] string origin,
                                                                       [FromQuery] string destination,
                                                                       [FromQuery] DateTime? departureDate)
        {
            var flights = await _flightService.SearchAsync(origin, destination, departureDate);
            return Ok(flights);
        }


        private readonly IFlightService _flightService;
    }
}
