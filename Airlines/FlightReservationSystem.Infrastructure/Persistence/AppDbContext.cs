using FlightReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlightReservationSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Flight)
                .WithMany(f => f.Reservations)
                .HasForeignKey(r => r.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        // Seed flights
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<AppDbContext>();
            if (context.Flights == null) throw new Exception("Flights DbSet is null");

            if (!context.Database.CanConnect())
            {
                context.Database.Migrate();
            }

            if (!context.Flights.Any()) 
            {
                context.Flights.AddRange(
                    new Flight
                    {
                        Id = Guid.NewGuid(),
                        FlightNumber = "IR700",
                        Origin = "Tehran",
                        Destination = "Mashhad",
                        DepartureTime = new DateTime(2025, 3, 27, 8, 0, 0),
                        ArrivalTime = new DateTime(2025, 3, 27, 9, 30, 0),
                        AvailableSeats = 150
                    },
                    new Flight
                    {
                        Id = Guid.NewGuid(),
                        FlightNumber = "IR701",
                        Origin = "Tehran",
                        Destination = "Shiraz",
                        DepartureTime = new DateTime(2025, 3, 27, 10, 0, 0),
                        ArrivalTime = new DateTime(2025, 3, 27, 11, 45, 0),
                        AvailableSeats = 120
                    },
                    new Flight
                    {
                        Id = Guid.NewGuid(),
                        FlightNumber = "IR702",
                        Origin = "Isfahan",
                        Destination = "Tabriz",
                        DepartureTime = new DateTime(2025, 3, 28, 14, 0, 0),
                        ArrivalTime = new DateTime(2025, 3, 28, 15, 30, 0),
                        AvailableSeats = 100
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
