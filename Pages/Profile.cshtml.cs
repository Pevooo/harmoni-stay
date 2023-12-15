using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainProject.Pages
{
    public class ProfileModel : PageModel
    {
        public readonly Context db;
        public ProfileModel(Context db)
        {
            this.db = db;
            Acc = new();
            Fac = new Facility();
            Emp = new();
        }
        public string Job;
        public Account Acc;
        public Employee Emp;
        public Facility Fac;
        public void OnGet()
        {
            var account=db.Accounts.FirstOrDefault(x=>x.AccountEmployee.EmployeeID==Globals.UserId);
            var emp = db.Employees.SingleOrDefault(e => e.EmployeeID == Globals.UserId);
            Acc = account;
            Emp = emp;
           // var fac=db.Facilities.SingleOrDefault(e => e.Facility==); 
            
         //  Fac= fac;


        }
    }
}
