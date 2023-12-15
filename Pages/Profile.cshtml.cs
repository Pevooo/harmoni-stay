using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainProject.Pages
{
    public class ProfileModel : PageModel
    {
        public readonly Context db;

        public string Job { get; set; }
        public Account Acc { get; set; }
        public Employee Emp { get; set; }
        public Facility Fac { get; set; }
        public ProfileModel(Context db)
        {
            this.db = db;
            Acc = new();
            Fac = new();
            Emp = new();
        }

        public void OnGet()
        {
            Acc =db.Accounts.SingleOrDefault(x => x.AccountEmployee.EmployeeID == Globals.UserId);
            Emp = db.Employees.SingleOrDefault(e => e.EmployeeID == Globals.UserId);
            Fac = db.Facilities.SingleOrDefault(item => item.FacilityEmployee.Contains(Emp));
        }
    }
}
