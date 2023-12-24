using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace MainProject.Pages
{
    public class AddEmployeeModel : PageModel
    {
        public readonly Context db;
        public int Error { get; set; }
        string EmployeeName { get; set; }
        double EmployeeSalary { get; set; }
        double WorkingHours { get; set; }
        byte[] Image { get; set; }
        string FacilityName { get; set; }     
        MemoryStream MemoryStream { get; set; }
        public List<string> CategoryFacilities { set; get; }
        public AddEmployeeModel(Context db)
        {
            MemoryStream = new();
            this.db = db;
            CategoryFacilities = new();
            Error = 0;
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/Login", false, true);
                return;
            }
            foreach (var category in db.Facilities)
            {
                CategoryFacilities.Add(category.FacilityName);
            }
        }
        public IActionResult OnPost()
        {
            try
            {
                Employee emp = new();
                emp.EmployeeID = Request.Form["EmployeeID"];
                EmployeeName = Request.Form["EmployeeName"];
                WorkingHours = double.Parse(Request.Form["WorkingHours"]);
                EmployeeSalary = double.Parse(Request.Form["EmployeeSalary"]);
                Request.Form.Files[0].CopyTo(MemoryStream);
                FacilityName = Request.Form["Facility"];
                var check =db.Employees.Any(x=>x.EmployeeID==emp.EmployeeID);
                if (check)
                {
                    Error = 1;
                    return Page();
                }

                foreach (var item in Request.Form)
                {
                    if (Request.Form[item.Key].IsNullOrEmpty())
                    {
                        Error = 1;
                        return Page();
                    }
                }

                var facilityId = db.Facilities.Where(x => x.FacilityName == FacilityName);
                Image = MemoryStream.ToArray();
                emp.EmployeeSalary = EmployeeSalary;
                emp.WorkingHours = WorkingHours;
                emp.EmployeeName = EmployeeName;
                emp.Image = Image;
                emp.EmployeeFacility = facilityId.First();
                db.Add(emp);
                db.SaveChanges();
                Error = 2;
               // return RedirectToPage("/Staff");

            }
            catch
            {
                Error = 1;

            }
            return Page();
        }
    }
}
