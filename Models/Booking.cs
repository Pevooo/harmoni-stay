using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CheckIn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CheckOut { get; set; }
        public virtual Room BookingRoom { get; set; }
        public virtual Guest BookingGuest { get; set; }
    }
}
