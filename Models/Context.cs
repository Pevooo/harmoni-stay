using MainProject;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=localhost;Database=Harmonistay;Trusted_Connection=True;TrustServerCertificate=True");
        }
        DbSet<Room> Rooms { get; set; }
        DbSet<Facility> Facilities { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Guest> Guests { get; set; }
        public DbSet<Account> Accounts { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Booking> Bookings { get; set; }
        
    }
}
