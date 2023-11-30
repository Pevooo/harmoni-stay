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
        public FacilitiesModel()
        {
            db = new();
            timeoffacilities=new(); Facilitiesnames=new();
        }
        //public void OnPost()
        //{
        //    foreach (var facility in db.Facilities)
        //    {
        //        string st = facility.FacilityWorkStart.TimeOfDay.ToString();
        //        string ed = facility.FacilityWorkEnd.TimeOfDay.ToString();
        //        string name = facility.FacilityName;
        //        timeoffacilities.Add(st);
        //        timeoffacilities.Add(ed);
        //        Facilitiesnames.Add(name);



        //    }
        //}
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
                


            }
        }
        private readonly Context db;
        public List<string> timeoffacilities {  get; set; }
        public List<string> Facilitiesnames { get; set; }   
        

       
    }
}
