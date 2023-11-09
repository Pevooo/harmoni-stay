namespace MainProject
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public double EmployeeSalary { get; set; }

        public double WorkingHours { get; set; }
        public string Facility { get; set; }

        public virtual Facility FacilityEmployee { get; set; }
    }
}
