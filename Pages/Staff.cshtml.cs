using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace MainProject.Pages
{
    public class StaffModel : PageModel
    {
        // The Fields Which is Will be entered
        public readonly Context db;
        public string? Category { get; set; }
        public List<string> CategoryFacilities { set; get; }
        public List<Employee> Emps { set; get; }
        public Dictionary<string,string> Tags { set; get; }
        // initialize some variables
        public StaffModel(Context db)
        {
            this.db = db;
            CategoryFacilities = new();
            Emps = new();
            Tags = new();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null || (HttpContext.Session.GetString("UserType") != "manager"))
            {
                return RedirectToPage("/Login");
            }
           
            foreach (var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
            }
            return Page();
        }
        // Add Employee Form 
        public void OnPost()
        {         
            Category = Request.Form["Facility"];
            var query = db.Employees.Where(item => item.EmployeeFacility.FacilityName == Category).Select(x => x);
            Emps = query.ToList();
            foreach(var item in Emps)
            {
                try
                {
                    string ImageURL = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(item.Image));
                    Tags.Add(item.EmployeeID, ImageURL);
                }
                catch
                {
                    Tags.Add(item.EmployeeID, null);
                }
            }
            foreach (var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
            }
        }
    }
}
