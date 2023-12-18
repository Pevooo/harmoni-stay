using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Runtime.Intrinsics.Arm;
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
        public IActionResult OnPost(int id)
        {
            try
            {
                var fac1 = db.Facilities.FirstOrDefault(x => x.FacilityID == id);
             
                FacilityName = Request.Form["FacilityName"];
                st = TimeSpan.Parse( Request.Form["startDate"]);
                end = TimeSpan.Parse(Request.Form["endDate"]);

                Request.Form.Files.First().CopyTo(memoryStream);
                photo=memoryStream.ToArray();

                Request.Form.Files[0].CopyTo(memoryStream);
                photo = memoryStream.ToArray();
                fac1.FacilityName = FacilityName;
                fac1.FacilityWorkStart = DateTime.Today.Add(st);
                fac1.FacilityWorkEnd = DateTime.Today.Add(end);
                fac1.Image = photo;
                db.SaveChanges();
                return RedirectToPage("/Facilities");
            }
            catch
            {
                Error = true;
            }
            return Page();
        }

    }
}
