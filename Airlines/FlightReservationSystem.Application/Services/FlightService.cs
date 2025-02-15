using FlightReservationSystem.Application.DTOs;
using FlightReservationSystem.Application.Interfaces;
using FlightReservationSystem.Domain.Interfaces;

namespace FlightReservationSystem.Application.Services
{
    public class FlightService : IFlightService
    {
        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<IEnumerable<FlightDto>> SearchFlightsAsync(string origin, string destination, DateTime? departureDate)
        {
            var flights = await _flightRepository.SearchAsync(origin, destination, departureDate);
            return flights.Select(f => new FlightDto
            {
                Id = f.Id,
                FlightNumber = f.FlightNumber,
                Origin = f.Origin,
                Destination = f.Destination,
                DepartureTime = f.DepartureTime,
                ArrivalTime = f.ArrivalTime,
                AvailableSeats = f.AvailableSeats
            });
        }

        public async Task<FlightDto> GetFlightByIdAsync(Guid flightId)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            return new FlightDto
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                Origin = flight.Origin,
                Destination = flight.Destination,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                AvailableSeats = flight.AvailableSeats
            };
        }


        private readonly IFlightRepository _flightRepository;
    }
}
