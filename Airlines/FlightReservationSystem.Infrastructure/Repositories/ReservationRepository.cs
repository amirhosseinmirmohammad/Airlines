﻿using FlightReservationsSystem.Domain.Entities;
using FlightReservationsSystem.Domain.Interfaces;
using FlightReservationsSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsSystem.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId)
        {
            return await _context.Reservations.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(Guid reservationId)
        {
            return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId); 
        }

        public async Task DeleteReservationByIdAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);

            await _context.SaveChangesAsync();
        }


        private readonly AppDbContext _context;
    }
}
