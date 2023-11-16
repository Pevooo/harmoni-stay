using MainProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Models
{
    public class Guest
    {
        public int GuestID { get; set; }
        public string GuestName { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string GuestNationality { get; set; }
        public virtual ICollection<Booking> GuestBookings { get; set; }
    }
}
