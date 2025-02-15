using FlightReservationSystem.Application.DTOs;
using FlightReservationSystem.Application.Interfaces;
using FlightReservationSystem.Domain.Entities;
using FlightReservationSystem.Domain.Interfaces;

namespace FlightReservationSystem.Application.Services
{
    public class ReservationService : IReservationService
    {
        public ReservationService(IFlightRepository flightRepository, IReservationRepository reservationRepository)
        {
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationDto> CreateReservationAsync(Guid flightId, Guid userId)
        {
            await _semaphore.WaitAsync();
            try
            {
                var flight = await _flightRepository.GetByIdAsync(flightId);
                if (flight == null)
                    throw new Exception("Flight not found");

                if (!flight.ReserveSeat(1))
                    throw new Exception("No available seats");

                await _flightRepository.UpdateAsync(flight);

                var reservation = new Reservation(flightId, userId);
                await _reservationRepository.AddAsync(reservation);

                return new ReservationDto
                {
                    Id = reservation.Id,
                    FlightId = reservation.FlightId,
                    UserId = reservation.UserId,
                    ReservationDate = reservation.ReservationDate
                };
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(Guid userId)
        {
            var reservations = await _reservationRepository.GetReservationsByUserAsync(userId);
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                FlightId = r.FlightId,
                UserId = r.UserId,
                ReservationDate = r.ReservationDate
            });
        }

        public async Task CancelReservationAsync(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId); 

            if (reservation == null)
                throw new Exception("Reservation not found");

            await _reservationRepository.DeleteReservationByIdAsync(reservation);
        }

        public Task<ReservationDto> GetReservationByIdAsync(Guid reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationDto> UpdateReservationAsync(Guid reservationId, DateTime newDepartureDate, int newSeats)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new Exception("Reservation not found");

            var flight = await _flightRepository.GetByIdAsync(reservation.FlightId);
            if (flight == null)
                throw new Exception("Flight not found");

            flight.DepartureTime = newDepartureDate;

            if (!flight.ReserveSeat(newSeats)) 
                throw new Exception("Not enough available seats");

            await _flightRepository.UpdateAsync(flight);

            reservation.ReservationDate = newDepartureDate;

            await _reservationRepository.AddAsync(reservation);

            return new ReservationDto
            {
                Id = reservation.Id,
                FlightId = reservation.FlightId,
                UserId = reservation.UserId,
                ReservationDate = reservation.ReservationDate
            };
        }

        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    }
}
