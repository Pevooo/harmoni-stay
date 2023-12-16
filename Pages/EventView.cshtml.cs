using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using MainProject.Models;
using System.Linq;

namespace MainProject.Pages
{
    public class EventViewModel : PageModel
    {
        public List<Event> eventList { get; set; }
        public int eventViewId{ get; set; }

        public string EventType { get; set; }

       public  List<Event> q { get; set; }

        private readonly Context db;
        public EventViewModel(Context db)
        {
            this.db = db;
            eventList = new();
        }

        
        public void OnGet(string id)
        {
            try
            {
                List<string> eventTypeNames = db.Events.Select(ev => ev.EventType).Distinct().ToList();

                if (eventTypeNames.Contains(id))
                {
                    q = db.Events.Where(ev => ev.EventType == id).ToList();
                    EventType = id;
                }
                else
                {
                    q = db.Events.ToList();
                    EventType = "All Events";
                }
            }
            catch 
            { 
             
            }
        }
    }
    
}
