using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public double EventFee { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventEnd { get; set; }
        public virtual Facility EventFacility { get; set; }


    }
}
