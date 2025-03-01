using Microsoft.AspNetCore.Identity;

namespace FlightReservationsSystem.Domain.Entities
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

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
