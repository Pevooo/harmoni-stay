using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace MainProject.Pages
{
    public class FacilitiesModel : PageModel
    {
        public string Error { get; set; }
        string Name { get; set; }
        private DateTime Start { get; set; }
        private DateTime End { get; set; }
        private readonly Context db;
        public List<int> IDSfacilities { get; set; }
        public List<string> Timeoffacilities { get; set; }
        public List<string> Facilitiesnames { get; set; }
        public List<string>Facilitiesphotos { get; set; }
        MemoryStream memoryStream = new MemoryStream();
        public byte[] photo {  get; set; }
        public FacilitiesModel(Context db)
        {
            this.db = db;
            Timeoffacilities=new(); Facilitiesnames=new();
            Facilitiesphotos=new();
            IDSfacilities=new();
         //   Error = null;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                Name= Request.Form["FacilityName"];
                
                Start = DateTime.Parse(Request.Form["startDate"]);
                End = DateTime.Parse(Request.Form["endDate"]);
                Request.Form.Files.First().CopyTo(memoryStream);
                Facility fac = new Facility();
                fac.FacilityName = Name;
                fac.FacilityWorkStart = Start;
                fac.FacilityWorkEnd = End;
                photo = memoryStream.ToArray();
                fac.Image = photo;
                if (Name == "" || Start == null || End == null || photo == null)
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
      

        public void OnGet()
        {
          //  Error = null;
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/Login", false, true);
                return;
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
        }
  
        

       
    }
}
