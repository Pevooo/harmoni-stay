using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public double Price { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime End { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }


    }
}
