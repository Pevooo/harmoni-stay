using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
        public Facility fac {  get; set; }
       public string FacilityName { get; set; }

        public EventsModel(Context db)
        {
            this.db = db;
            Events = new List<Event>();
            Facilities = new();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                return RedirectToPage("/Login");
            }
           
            Events = db.Events.ToList();
           Facilities=db.Facilities.ToList();
            return Page();
        }
        public void OnPost()
        {
            Events = db.Events.ToList();

            try
            {
                this.StartDate = DateTime.Parse(Request.Form["startDate"].ToString());
                this.EndDate = DateTime.Parse(Request.Form["endDate"].ToString());
                this.EventType = Request.Form["eventType"];
                this.EventName = Request.Form["eventName"];
                this.EventFee = double.Parse(Request.Form["EventFee"]);
                this.FacilityName = Request.Form["Facility"];
                var facilityId = db.Facilities.Where(x => x.FacilityName == FacilityName);
                    
               fac=facilityId.FirstOrDefault();

            }
            catch
            {
                Error = true;
                return;
            }
            if (this.EndDate < this.StartDate)
            {
                Error = true;
                Message = "Invalid Dates";
                return;
            }
            var invalidEvent = db.Events.Any(ev => StartDate < ev.EventEnd && EndDate > ev.EventStart);
            if (!invalidEvent)
            {
                Event ev=new Event();
                ev.EventType
                    = this.EventType;   
                ev.EventName = this.EventName;
                ev.EventStart = this.StartDate;
                ev.EventEnd = this.EndDate;
                ev.EventFacility = this.fac;
                ev.EventFee = this.EventFee;
                db.Events.Add(ev);
                db.SaveChanges();
                Message = $"{EventName} added";

            }
            else
            {
                Error = true;
                Message = "Your selected period is occupied for anther event";
            }

        }
    }
}
