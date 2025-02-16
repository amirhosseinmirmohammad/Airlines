using Microsoft.AspNetCore.Identity;

namespace FlightReservationSystem.Domain.Entities
{
    public class User : IdentityUser
    {
        public User() { }

        public User(string fullName, string email, string password)
        {
            FullName = fullName;
            UserName = email;
            Email = email;
            PasswordHash = password;
        }

        public string FullName { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
