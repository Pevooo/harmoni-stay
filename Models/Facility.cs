using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject.Models
{
    public class Facility
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FacilityWorkStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FacilityWorkEnd { get; set; }
        public string URL { get; set; }
        public virtual ICollection<Event> FacilityEvent { get; set; }
        public virtual ICollection<Employee> FacilityEmployee { get; set; }
        
    }
}
