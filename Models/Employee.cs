namespace MainProject.Models
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public double EmployeeSalary { get; set; }
        public byte[] Image { get; set; }
        public double WorkingHours { get; set; }
        public virtual Facility EmployeeFacility { get; set; }
    }
}
