using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Policy;

namespace MainProject.Pages
{
    public class EventsModel : PageModel
    {


        public string conference { get; set; }
        public string wedding { get; set; }
        public string concert { get; set; }
        public bool Error { get; set; }
        public string Name {  get; set; }
        private DateTime st { get; set; }
        private DateTime end { get; set; }
        private string type { get; set; }
        private readonly Context db;
        public Event NewEvent { get; set; }

        public EventsModel(Context db)
        {
            this.db = db;
        }
        public void OnGet()
        {
           
        }

        public void Onpost()
        {
            try
            {
                Name = Request.Form["event Name"];
                st = DateTime.Parse(Request.Form["startDate"]);
                end = DateTime.Parse(Request.Form["endDate"]);
                type= Request.Form["event Type"];
                Event ev = new Event();
                ev.EventName = Name;
                ev.EventStart = st;
                ev.EventEnd= end;
                ev.EventType = type;
                db.Events.Add(ev);
                db.SaveChanges();
            }
            catch
            {
                Error = true;
            }
            
        }
    }
}
