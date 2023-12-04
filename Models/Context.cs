using MainProject;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
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
