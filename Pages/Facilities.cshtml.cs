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
        private readonly Context db;
        public List<string> timeoffacilities { get; set; }
        public List<string> Facilitiesnames { get; set; }
        public List<string>Facilitiesphotos { get; set; }
        public FacilitiesModel(Context db)
        {
            this.db = db;
            timeoffacilities=new(); Facilitiesnames=new();
            Facilitiesphotos=new();
        }
        public void OnGet()
        {
            foreach (var facility in db.Facilities)
            {
                string st = facility.FacilityWorkStart.TimeOfDay.ToString();
                string ed = facility.FacilityWorkEnd.TimeOfDay.ToString();
                string name = facility.FacilityName;
                timeoffacilities.Add(st);
                timeoffacilities.Add(ed);
                Facilitiesnames.Add(name);
                Facilitiesphotos.Add(facility.URl);
                


            }
        }
  
        

       
    }
}
