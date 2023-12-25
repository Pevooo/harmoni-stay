using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;

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
        string Email { get; set; }
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
            if (HttpContext.Session.GetString("UserId") is null || (HttpContext.Session.GetString("UserType") != "manager"))
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
                foreach (var item in Request.Form)
                {
                    if (Request.Form[item.Key].IsNullOrEmpty())
                    {
                        Error = true;
                        return Page();
                    }
                }
                var emp = db.Employees.FirstOrDefault(x => x.EmployeeID == id);
                EmployeeName = Request.Form["EmployeeName"];
                WorkingHours = double.Parse(Request.Form["WorkingHours"]);
                EmployeeSalary = double.Parse(Request.Form["EmployeeSalary"]);
                Request.Form.Files[0].CopyTo(MemoryStream);
                FacilityName = Request.Form["Facility"];
                Email = Request.Form["Email"];
                var emailObject = new MailAddress(Email); // Will throw an exception if not valid, which will be cuaght
                var facilityId = db.Facilities.Where(x => x.FacilityName == FacilityName);
                Image = MemoryStream.ToArray();
                emp.EmployeeSalary = EmployeeSalary;
                emp.WorkingHours = WorkingHours;
                emp.EmployeeName = EmployeeName;
                emp.Image = Image;
                emp.EmployeeFacility = facilityId.First();
                emp.EmplooyeeEmail = Email;
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
