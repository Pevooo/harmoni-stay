using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;

namespace MainProject.Pages
{
    public class ProfileModel : PageModel
    {
        public readonly Context db;

        public string Job { get; set; }
        public Account Acc { get; set; }
        public Employee Emp { get; set; }
        public Facility Fac { get; set; }
        public TimeSpan Start {  get; set; }
        public TimeSpan End {  get; set; }
        public string Photo {  get; set; }
        public ProfileModel(Context db)
        {
            this.db = db;
            Acc = new();
            Fac = new();
            Emp = new();
        }
        // Retrieve Inforamtion about the User  
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/Login", false, true);
                return;
            }
            Acc =db.Accounts.SingleOrDefault(x => x.AccountEmployee.EmployeeID == HttpContext.Session.GetString("UserId"));
            Emp = db.Employees.SingleOrDefault(e => e.EmployeeID == HttpContext.Session.GetString("UserId"));
            if (Emp.Image != null)
            {
                Photo = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(Emp.Image));
            }
            Fac = db.Facilities.SingleOrDefault(item => item.FacilityEmployee.Contains(Emp));
            Start= Fac.FacilityWorkStart.TimeOfDay;
            End = Fac.FacilityWorkEnd.TimeOfDay;
        }
    }
}
