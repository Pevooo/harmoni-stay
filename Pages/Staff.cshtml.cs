using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace MainProject.Pages
{
    public class StaffModel : PageModel
    {
        public readonly Context db;
        public string category;
        public List<string> URlPhotos { set; get; }
        public List<string> CategoryFacilities { set; get; }
        public List<Employee> Emps { set; get; }
        public Dictionary<string,string> Tags { set; get; }
        public StaffModel(Context db)
        {
            this.db = db;
            CategoryFacilities = new();
            Emps = new();
            Tags = new();
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") is null)
            {
                Response.Redirect("/", false, true);
            }
            foreach (var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
            }

        }
        MemoryStream memoryStream = new MemoryStream();
        public void OnPost()
        {         
            category = Request.Form["Facility"];
            var query = db.Employees.Where(item => item.EmployeeFacility.FacilityName == category).Select(x=>x);
            Emps=query.ToList();
            foreach(var item in Emps)
            {
                string ImageURL = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(item.Image));
                Tags.Add(item.EmployeeID, ImageURL);
            }
            foreach (var item in db.Facilities)
            {
                CategoryFacilities.Add(item.FacilityName);
            }
        }
    }
}
