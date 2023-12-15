using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Policy;

namespace MainProject.Pages
{
    public class EventsModel : PageModel
    {


        //public BookingModel(Context db)
        //{
        //    this.db = db;
        //    Rooms = new List<Room>();
        //}



        //public string conference { get; set; }
        //public string wedding { get; set; }
        //public string concert { get; set; }
        ///// <summary>
        ///// ///////
        ///// </summary>
        //public bool error = false;
        //string Name;

       


        public bool Error = false;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string eventType { get; set; }
        public string eventName { get; set; }
        public string msg { get; set; }
        public int SelectedEvnt { get; set; }
        public List<Event> Events { get; set; }
        public Event e { get; set; }
        private readonly Context db;



        public Event newEvent { get; set; }

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
            }
            Events = db.Events.ToList();

        }
        public void OnPost()
        {
            //try
            //{
            //    Name = Request.Form["event Name"];
            //    st = DateTime.Parse(Request.Form["startDate"]);
            //    end = DateTime.Parse(Request.Form["endDate"]);
            //    type= Request.Form["event Type"];
            //    Event ev = new Event();
            //    ev.EventName = Name;
            //    ev.EventStart = st;
            //    ev.EventEnd= end;
            //    ev.EventType = type;
            //    _db.Events.Add(ev);
            //    _db.SaveChanges();
            //    return RedirectToAction("Events");
            //}
            //catch
            //{
            //    error = true;
            //    return Page();

            //}

            try
            {
                startDate = DateTime.Parse(Request.Form["startDate"].ToString());
                endDate = DateTime.Parse(Request.Form["endDate"].ToString());
                eventType = Request.Form["event Type"];
                eventName = Request.Form["event Type"];
                
            }
            catch
            {
                Error = true;
                return;
            }
            var validEvent = db.Events.Any(x => (eventType == x.EventType &&( (startDate < x.EventStart && endDate < x.EventStart)|| (startDate > x.EventStart && endDate > x.EventStart))));
            if (validEvent)
            {
                
                    e.EventName = eventName;
                    e.EventEnd = startDate;
                    e.EventType = eventType;
                    e.EventStart = startDate;
                    db.Events.Add(e);
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
