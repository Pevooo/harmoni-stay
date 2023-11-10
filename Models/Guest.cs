using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject
{
    public class Guest
    {
        public int GuestID { get; set; }
        public string GuestName { get; set; }
        public string GustRoomType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CheckIn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CheckOut{ get; set; }
        public virtual Room GuestRoom { get; set; }
    }
}
