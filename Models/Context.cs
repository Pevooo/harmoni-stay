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
        public DbSet<Room> Rooms { get; set; }
		public DbSet<Facility> Facilities { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Guest> Guests { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        
    }
}
