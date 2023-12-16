using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MainProject.Pages
{
    public class EditFacilitiesModel : PageModel
    {
        private readonly Context db;
        public EditFacilitiesModel(Context db)
        {
            this.db = db;
          
        }
        public string FacilityName { get; set; }
        public int Id { get; set; }
        public TimeSpan st { get; set; }
        public TimeSpan end { get; set; }

        MemoryStream memoryStream = new MemoryStream();
        public byte[] photo { get; set; }
        public bool Error {  get; set; }
        public void OnGet(int id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/Login", false, true);
                return;
            }
            var fac1=db.Facilities.FirstOrDefault(x=>x.FacilityID==id);

            FacilityName = fac1.FacilityName;
            Id = fac1.FacilityID;

            st = fac1.FacilityWorkStart.TimeOfDay;
            end = fac1.FacilityWorkEnd.TimeOfDay;
            photo = fac1.Image;


        }
        public void OnPost(int id)
        {
            try
            {
                var fac1 = db.Facilities.FirstOrDefault(x => x.FacilityID == id);

                FacilityName = Request.Form["FacilityName"];
                st = TimeSpan.Parse( Request.Form["startDate"]);
                end = TimeSpan.Parse(Request.Form["endDate"]);
                // URL = Request.Form["PictureURL"];
                Request.Form.Files.First().CopyTo(memoryStream);
                photo=memoryStream.ToArray();
                Facility fac = new Facility();
                fac1.FacilityName = FacilityName;
                DateTime f1 = DateTime.Today.Add(st);
                DateTime f2 = DateTime.Today.Add(end);
                fac1.FacilityWorkStart = f1;
                fac1.FacilityWorkEnd = f2;
                fac1.Image = photo;
             
                db.SaveChanges();
                Response.Redirect("/Facilities", false, true);
            }
            catch
            {
                Error = true;
                return;

            }

        }

    }
}
