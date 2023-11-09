using MainProject;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=localhost;user id=MSI\johna;Database=Harmonistay;Trusted_Connection=True;trustservercertificate=True");
        }
        DbSet<Room> Rooms { get; set; }
        DbSet<Facility> Facilities { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Guest>Guests { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Transaction> Transactions { get; set; }


    }
}
