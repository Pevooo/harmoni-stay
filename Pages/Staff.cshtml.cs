using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Intrinsics.Arm;

namespace MainProject.Pages
{
    public class StaffModel : PageModel
    {
        public readonly Context db;
        public string category;
        public List<string> CategoryFacilities { set; get; }
        public List<Employee> Emps { set; get; }
        public StaffModel(Context db)
        {
            this.db = db;
            CategoryFacilities = new();
            Emps = new();
        }
        public void OnGet()
        {
            foreach(var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
            }

        }

        public void OnPost()
        {
           
            category = Request.Form["Facility"];
            var query = db.Employees.Where(item => item.FacilityEmployee.FacilityName == category).Select(x=>x);
            Emps=query.ToList();
            foreach (var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
            }
        }
    }
}
