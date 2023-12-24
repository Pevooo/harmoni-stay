using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Policy;

namespace MainProject.Pages
{
    public class EditEventModel : PageModel
    {
        public List<Facility> Facilities { set; get; }
        private readonly Context db;
        public EditEventModel(Context db)
        {
            this.db = db;
            Facilities = new List<Facility>();
           

        }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public int EventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        Facility Fac { get; set; }
        string FacilityName { get; set; }
        public double Fee { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
        public Event Curr {  get; set; }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                return RedirectToPage("/Login");
            }
           
            var eventToEdit = db.Events.FirstOrDefault(x => x.EventID == id);
            Curr = eventToEdit;
            Facilities=db.Facilities.ToList();
            EventName = eventToEdit.EventName;
            EventId = eventToEdit.EventID;
            EventType = eventToEdit.EventType;
            StartDate = eventToEdit.EventStart;
            EndDate = eventToEdit.EventEnd;
            Fee = eventToEdit.EventFee;
            Fac = eventToEdit.EventFacility;
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            try
            {
                
                var eventToEdit = db.Events.FirstOrDefault(x => x.EventID == id);

                EventName = Request.Form["EventName"];
                StartDate = DateTime.Parse(Request.Form["startDate"]);
                EndDate = DateTime.Parse(Request.Form["endDate"]);
                EventType = Request.Form["EventType"];
                Fee = double.Parse(Request.Form["EventFee"]);
                FacilityName = Request.Form["Facility"];
                var facilityId = db.Facilities.Where(x => x.FacilityName == FacilityName);
                Fac = facilityId.FirstOrDefault();

                foreach (var item in Request.Form)
                {
                    if (Request.Form[item.Key].IsNullOrEmpty())
                    {
                        Error = true;
                        return Page();
                    }
                }

                if (EndDate < StartDate)
                {
                    Error = true;
                    Message = "Invalid Dates";

                }
                else
                {
                    var invalidEvent = db.Events.Any(ev => StartDate < ev.EventEnd && EndDate > ev.EventStart);
                    if (!invalidEvent)
                    {
                        eventToEdit.EventName = EventName;
                        eventToEdit.EventStart = StartDate;
                        eventToEdit.EventEnd = EndDate;
                        eventToEdit.EventType = EventType;
                        eventToEdit.EventID = id;
                        eventToEdit.EventFee = Fee;
                        eventToEdit.EventFacility = Fac;
                        db.SaveChanges(); 
                        Message = $"Event edited";
                    }
                    else
                    {
                        Error = true;
                        Message = "Your selected period is occupied for anther event";
                    }

                }
            }
            catch
            {
                Error = true;
            }

            return RedirectToPage("/Events");

        }
    }
}