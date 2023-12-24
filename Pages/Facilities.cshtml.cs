using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace MainProject.Pages
{
    public class FacilitiesModel : PageModel
    {
        // The Fields Which is Will be entered
        public string Error { get; set; }
        string Name { get; set; }
        private DateTime Start { get; set; }
        private DateTime End { get; set; }
        private readonly Context db;
        public List<int> IDSfacilities { get; set; }
        public List<string> Timeoffacilities { get; set; }
        public List<string> Facilitiesnames { get; set; }
        public List<string>Facilitiesphotos { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public byte[] Photo {  get; set; }
        // initialize some variables
        public FacilitiesModel(Context db)
        {
            this.db = db;
            Timeoffacilities=new(); Facilitiesnames=new();
            Facilitiesphotos=new();
            IDSfacilities=new();
         //   Error = null;
        }
        // Add form 
        public IActionResult OnPost()
        {
            try
            {
                Name= Request.Form["FacilityName"];
                
                Start = DateTime.Parse(Request.Form["startDate"]);
                End = DateTime.Parse(Request.Form["endDate"]);
                Request.Form.Files[0].CopyTo(MemoryStream);
                Photo = MemoryStream.ToArray();
                Facility fac = new()
                {
                    FacilityName = Name,
                    FacilityWorkStart = Start,
                    FacilityWorkEnd = End,
                    Image = Photo
                };
               
                if (Name == "" || Photo == null)
                {
                    TempData["Error"] = "oops,error";
                    Error = "true";

                }
                else
                {
                    db.Facilities.Add(fac);
                    db.SaveChanges();
                }
               
               
            }
            catch
            {
                TempData["Error"] = "oops,error";
                Error = "true";
                
            }
            return RedirectToPage("Facilities");
        }
      

        public IActionResult OnGet()
        {
            //  Error = null;
            if (HttpContext.Session.GetString("UserId") is null)
            {
                return RedirectToPage("/Login");
            }
           
            foreach (var facility in db.Facilities)
            {
                string st = facility.FacilityWorkStart.ToString("H:mm");
                string ed = facility.FacilityWorkEnd.ToString("H:mm");
                string name = facility.FacilityName;
                IDSfacilities.Add(facility.FacilityID);
                Timeoffacilities.Add(st);
                Timeoffacilities.Add(ed);
                Facilitiesnames.Add(name);
                string src="";
                if(facility.Image != null)
                src= string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(facility.Image));

                Facilitiesphotos.Add(src);
                


            }
            return Page();
        }
  
        

       
    }
}
