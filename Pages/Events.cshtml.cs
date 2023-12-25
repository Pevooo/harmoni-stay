using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;

namespace MainProject.Pages
{
    public class EventsModel : PageModel
    {


        public List<Facility> Facilities { set; get; }

        public bool Error = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventType { get; set; }
        public string EventName { get; set; }
        public string Message { get; set; }
        public int SelectedEvnt { get; set; }
        public double EventFee { get; set; }
        public List<Event> Events { get; set; }
        public Event NewEvent { get; set; }
        private readonly Context db;
        public Facility Fac { get; set; }
        public string FacilityName { get; set; }

        public EventsModel(Context db)
        {
            this.db = db;
            Events = new List<Event>();
            Facilities = new();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null || (HttpContext.Session.GetString("UserType") != "manager"))
            {
                return RedirectToPage("/Login");
            }
           
            Events = db.Events.ToList();
            Facilities = db.Facilities.ToList();
            return Page();
        }
        public IActionResult OnPost()
        {
            Events = db.Events.ToList();

            try
            {
                foreach (var item in Request.Form)
                {
                    if (Request.Form[item.Key].IsNullOrEmpty())
                    {
                        Error = true;
                        return Page();
                    }
                }
                this.StartDate = DateTime.Parse(Request.Form["startDate"].ToString());
                this.EndDate = DateTime.Parse(Request.Form["endDate"].ToString());
                this.EventType = Request.Form["eventType"];
                this.EventName = Request.Form["eventName"];
                this.EventFee = double.Parse(Request.Form["EventFee"]);
                this.FacilityName = Request.Form["Facility"];
                var facilityId = db.Facilities.Where(x => x.FacilityName == FacilityName);
                    
                Fac = facilityId.FirstOrDefault();

            }
            catch
            {
                Error = true;
                return Page();
            }
            if (this.EndDate < this.StartDate)
            {
                Error = true;
                Message = "Invalid Dates";
                return Page();
            }
            var invalidEvent = db.Events.Any(ev => StartDate < ev.EventEnd && EndDate > ev.EventStart);
            if (!invalidEvent)
            {
                Event ev = new()
                {
                    EventType = this.EventType,
                    EventName = this.EventName,
                    EventStart = this.StartDate,
                    EventEnd = this.EndDate,
                    EventFacility = this.Fac,
                    EventFee = this.EventFee
                };
                db.Events.Add(ev);
                db.SaveChanges();
                Message = $"{EventName} added";
                return Page();
            }
            else
            {
                Error = true;
                Message = "Your selected period is occupied for anther event";
                return Page();
            }

        }
    }
}
