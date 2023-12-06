using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MainProject.Models;
using System.Diagnostics;

namespace MainProject.Pages
{
    public class FacilitiesModel : PageModel
    {
        public bool Error = false;
        string Name;
        private string URL { get; set; }
        private DateTime st { get; set; }
        private DateTime end { get; set; }
        private readonly Context db;
        public List<int> IDSfacilities { get; set; }
        public List<string> timeoffacilities { get; set; }
        public List<string> Facilitiesnames { get; set; }
        public List<string>Facilitiesphotos { get; set; }
        public FacilitiesModel(Context db)
        {
            this.db = db;
            timeoffacilities=new(); Facilitiesnames=new();
            Facilitiesphotos=new();
            IDSfacilities=new();
        }
       
        public IActionResult OnPost()
        {
            try
            {
                Name= Request.Form["FacilityName"];
                st = DateTime.Parse(Request.Form["startDate"]);
                end = DateTime.Parse(Request.Form["endDate"]);
                URL = Request.Form["PictureURL"];
                Facility fac = new Facility();
                fac.FacilityName = Name;
                fac.FacilityWorkStart = st;
                fac.FacilityWorkEnd = end;
                fac.URL = URL;
                db.Facilities.Add(fac);
                db.SaveChanges();
               
            }
            catch
            {
                Error = true;
                return RedirectToAction("Index");
                
            }
            return RedirectToAction("Index");
        }
      

        public void OnGet()
        {
            foreach (var facility in db.Facilities)
            {
                string st = facility.FacilityWorkStart.ToString("H:mm");
                string ed = facility.FacilityWorkEnd.ToString("H:mm");
                string name = facility.FacilityName;
                IDSfacilities.Add(facility.FacilityID);
                timeoffacilities.Add(st);
                timeoffacilities.Add(ed);
                Facilitiesnames.Add(name);
                Facilitiesphotos.Add(facility.URL);
                


            }
        }
  
        

       
    }
}
