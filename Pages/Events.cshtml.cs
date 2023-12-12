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
        /// <summary>
        /// ///////
        /// </summary>
        public bool error = false;
        string Name;
        
        private DateTime st { get; set; }
        private DateTime end { get; set; }
        private string type { get; set; }
        private readonly Context _db;

        public Event newEvent { get; set; }

        public EventsModel(Context db)
        {
            _db = db;
        }
        public void OnGet()
        {
           
        }

        public IActionResult Onpost()
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
                _db.Events.Add(ev);
                _db.SaveChanges();
                return RedirectToAction("Events");
            }
            catch
            {
                error = true;
                return Page();

            }
            
        }
        public async Task<IActionResult>  OnPostAsync()
        {
            return RedirectToPage("/EventView");

        }
    }
}
