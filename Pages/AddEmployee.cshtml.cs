using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainProject.Pages
{
    public class AddEmployeeModel : PageModel
    {
        public bool Error = false;
        public readonly Context db;
        string EmployeeName;
        double EmployeeSalary;
        double WorkingHours;
        byte[] Image;
        string FacilityName;     
        MemoryStream memoryStream = new MemoryStream();
        public List<string> CategoryFacilities { set; get; }
        public AddEmployeeModel(Context db)
        {
            this.db = db;
            CategoryFacilities = new();
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
                Employee emp = new Employee();
                emp.EmployeeID = Request.Form["EmployeeID"];
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
                db.Add(emp);
                db.SaveChanges();
                return RedirectToPage("/Staff");

            }
            catch
            {
                Error = true;

            }
            return Page();
        }
    }
}
