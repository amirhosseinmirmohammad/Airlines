using FlightReservationsSystem.Domain.Interfaces;
using FlightReservationsSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsSystem.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        public FlightRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Flight> GetByIdAsync(Guid id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<IEnumerable<Flight>> SearchAsync(string origin, string destination, DateTime? departureDate)
        {
            var query = _context.Flights.AsQueryable();

            if (!string.IsNullOrWhiteSpace(origin))
                query = query.Where(f => f.Origin.Contains(origin));

            if (!string.IsNullOrWhiteSpace(destination))
                query = query.Where(f => f.Destination.Contains(destination));

            if (departureDate.HasValue)
                query = query.Where(f => f.DepartureTime.Date == departureDate.Value.Date);

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Flight flight)
        {
            _context.Flights.Update(flight);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            var query = _context.Flights.AsQueryable();

            return await query.ToListAsync();
        }

        private readonly AppDbContext _context;
    }
}
