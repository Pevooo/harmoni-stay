using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MainProject.Pages
{
    public class EditStaffModel : PageModel
    {
        public readonly Context db;
        public bool Error { get; set; }
        public List<string> CategoryFacilities { set; get; }
        public List<Facility> Facilities { set; get; }
        public Employee Employee {  get; set; }

        string EmployeeName {  get; set; }
        double EmployeeSalary {  get; set; }
        double WorkingHours {  get; set; }
        byte[] Image {  get; set; }
        string FacilityName { get; set; }

        MemoryStream MemoryStream {  get; set; }

        public EditStaffModel(Context db)
        {
            this.db = db;
            CategoryFacilities = new();
            Employee = new();
            MemoryStream = new();
            Facilities = new();
        }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                return RedirectToPage("/Login");
            }
           
            foreach (var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
                Facilities.Add(item);
            }
            var query = db.Employees.Where(x=>x.EmployeeID == id).Select(x=>x);
            Employee = query.First();
            return Page();
        }
    

        public IActionResult OnPost(string id)
        {
            try
            {
                var emp = db.Employees.FirstOrDefault(x => x.EmployeeID == id);
                EmployeeName = Request.Form["EmployeeName"];
                WorkingHours = double.Parse(Request.Form["WorkingHours"]);
                EmployeeSalary = double.Parse(Request.Form["EmployeeSalary"]);
                Request.Form.Files[0].CopyTo(MemoryStream);
                FacilityName = Request.Form["Facility"];
                var facilityId = db.Facilities.Where(x => x.FacilityName == FacilityName);
                Image = MemoryStream.ToArray();
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
