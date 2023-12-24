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
            MemoryStream = new();
        }
        public string FacilityName { get; set; }
        public int Id { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        MemoryStream MemoryStream {  get; set; }
        public byte[] Photo { get; set; }
        public bool Error {  get; set; }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                return RedirectToPage("/Login");
            }
           
            var fac1=db.Facilities.FirstOrDefault(x=>x.FacilityID==id);

            FacilityName = fac1.FacilityName;
            Id = fac1.FacilityID;

            Start = fac1.FacilityWorkStart.TimeOfDay;
            End = fac1.FacilityWorkEnd.TimeOfDay;
            Photo = fac1.Image;
            return Page();

        }
        public IActionResult OnPost(int id)
        {
            try
            {
                var fac1 = db.Facilities.FirstOrDefault(x => x.FacilityID == id);
             
                FacilityName = Request.Form["FacilityName"];
                Start = TimeSpan.Parse( Request.Form["startDate"]);
                End = TimeSpan.Parse(Request.Form["endDate"]);

                Request.Form.Files.First().CopyTo(MemoryStream);
                Photo = MemoryStream.ToArray();

                Request.Form.Files[0].CopyTo(MemoryStream);
                Photo = MemoryStream.ToArray();
                fac1.FacilityName = FacilityName;
                fac1.FacilityWorkStart = DateTime.Today.Add(Start);
                fac1.FacilityWorkEnd = DateTime.Today.Add(End);
                fac1.Image = Photo;
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
