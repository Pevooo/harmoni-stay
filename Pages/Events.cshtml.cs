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
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string eventType { get; set; }
        public string eventName { get; set; }
        public string msg { get; set; }
        public int SelectedEvnt { get; set; }
        public List <Event> Events { get; set; }
        public Event newEvent { get; set; }
        private readonly Context db;



      

        public EventsModel(Context db)
        {
            this.db = db;
            Events = new List<Event>();
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") is null)
            {
                Response.Redirect("/", false, true);
                return;
            }
            Events = db.Events.ToList();

        }
        public void OnPost()
        {
            Events = db.Events.ToList();

            try
            {
                this.startDate = DateTime.Parse(Request.Form["startDate"].ToString());
                this.endDate = DateTime.Parse(Request.Form["endDate"].ToString());
                this.eventType = Request.Form["eventType"];
                this.eventName = Request.Form["eventName"];
                
            }
            catch
            {
                Error = true;
                return;
            }
            var validEvent = db.Events.Any(x => (eventType == x.EventType &&( (startDate < x.EventStart && endDate < x.EventStart)|| (startDate > x.EventStart && endDate > x.EventStart))));
            if (validEvent )
            {
                newEvent = new();
                newEvent.EventName = this.eventName;
                newEvent.EventEnd = this.startDate;
                newEvent.EventType = this.eventType;
                newEvent.EventStart = this.startDate;
                db.Events.Add(newEvent);
                db.SaveChanges();
                msg = $"{eventName} added";
               
            }
            else
            {
                Error = true;
                msg = "your selected period is occupied for anther event";
            }
           
        }
    }
}
