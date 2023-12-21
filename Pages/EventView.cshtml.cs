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
        public List<Event> EventList { get; set; }
        public int EventViewId{ get; set; }
        
        public string EventType { get; set; }

       public  List<Event> SlectedEventType  { get; set; }

        private readonly Context db;

        public bool Error = false;
        public string Message { get; set; }


        public EventViewModel(Context db)
        {
            this.db = db;
            EventList = new();
        }

        
        public void OnGet(string id)
        {
            try
            {
                List<string> eventTypeNames = db.Events.Select(ev => ev.EventType).Distinct().ToList();

                if (eventTypeNames.Contains(id) )
                {
                    SlectedEventType = db.Events.Where(ev => ev.EventType == id).ToList();
                    EventType = id;
                }
                else if (id== "All Events")
                {

                    SlectedEventType = db.Events.ToList();
                    EventType = "All Events";
                }
                else
                {
                    EventType = id;
                    Error = true;
                    Message = $"There is no event with type {EventType} added yet";
                }
            }
            catch 
            {
                Error = true;
                return;
            }
        }
    }
    
}
