using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MainProject.Pages
{
    public class EditStaffModel : PageModel
    {
        public readonly Context db;
        public string category;
        public bool Error;
        public List<string> CategoryFacilities { set; get; }
        public Employee employee;
        public EditStaffModel(Context db)
        {
            this.db = db;
            CategoryFacilities = new();
            employee = new Employee();
        }
        public void OnGet(string id)
        {
            if (HttpContext.Session.GetInt32("UserId") is null)
            {
                Response.Redirect("/", false, true);
            }
            foreach (var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
            }
            var query =db.Employees.Where(x=>x.EmployeeID == id).Select(x=>x);
            employee=query.First();
            
        }
    
        string EmployeeName;
        double EmployeeSalary;
        double WorkingHours;
        byte[] Image;
        string FacilityName; 
        MemoryStream memoryStream = new MemoryStream();
        public IActionResult OnPost(string id)
        {
            try
            {
                var emp = db.Employees.FirstOrDefault(x => x.EmployeeID == id);
                EmployeeName = Request.Form["EmployeeName"];
                WorkingHours = double.Parse(Request.Form["WorkingHours"]);
                EmployeeSalary = double.Parse(Request.Form["EmployeeSalary"]);
                Request.Form.Files.First().CopyTo(memoryStream);
                FacilityName = Request.Form["Facility"];
                var facilityId = db.Facilities.Where(x => x.FacilityName == FacilityName);
                Image = memoryStream.ToArray();
                emp.EmployeeSalary = EmployeeSalary;
                emp.WorkingHours = WorkingHours;
                emp.EmployeeName = EmployeeName;
                emp.Image = Image;
                emp.EmployeeFacility = facilityId.First();
                db.SaveChanges();
                return RedirectToPage("/Staff");
            }
            catch
            {
                Error = true;
                return Page();

            }

        }
    }
}
