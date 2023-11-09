using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject
{
    public class Facility
    {
        public int FacilityID { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime End { get; set; }
        public virtual Event Events { get; set; }
    }
}
