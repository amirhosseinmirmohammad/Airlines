using FlightReservationsSystem.Application.DTOs;
using FlightReservationsSystem.Application.Interfaces;
using FlightReservationsSystem.Domain.Interfaces;

namespace FlightReservationsSystem.Application.Services
{
    public class FlightService : IFlightService
    {
        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<IEnumerable<FlightDto>> SearchAsync(string origin, string destination, DateTime? departureDate)
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

        public async Task<FlightDto> GetByIdAsync(Guid flightId)
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

        public async Task<IEnumerable<FlightDto>> GetAllAsync()
        {
            var flights = await _flightRepository.GetAllAsync();
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


        private readonly IFlightRepository _flightRepository;
    }
}
