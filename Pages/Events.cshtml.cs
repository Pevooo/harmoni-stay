using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Policy;

namespace MainProject.Pages
{
    public class EventsModel : PageModel
    {




        public bool Error = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventType { get; set; }
        public string EventName { get; set; }
        public string Message { get; set; }
        public int SelectedEvnt { get; set; }
        public List<Event> Events { get; set; }
        public Event NewEvent { get; set; }
        private readonly Context db;





        public EventsModel(Context db)
        {
            this.db = db;
            Events = new List<Event>();
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/Login", false, true);
                return;
            }
            Events = db.Events.ToList();

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
                NewEvent = new()
                {
                    EventName = this.EventName,
                    EventStart = this.StartDate,
                    EventType = this.EventType,
                    EventEnd = this.EndDate
                };
                db.Events.Add(NewEvent);
                db.SaveChanges();
                Message = $"{NewEvent} added";

            }
            else
            {
                Error = true;
                Message = "Your selected period is occupied for anther event";
            }

        }
    }
}
