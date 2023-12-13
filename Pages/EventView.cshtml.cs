using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using MainProject.Models;
namespace MainProject.Pages
{
    public class EventViewModel : PageModel
    {
        public class EventInfo
        {
           public  string name{ get; set; }
            public string EventType { get; set; }
          
            public DateTime EventStart { get; set; }
           
            public DateTime EventEnd { get; set; }
        }
        public List<Event> eventList { get; set; }
        public int eventViewId{ get; set; }

        public int idEvent;
        
        private readonly Context db;
        public EventViewModel(Context db)
        {
            this.db = db;
            eventList = new();
        }

        
        public void OnGet(string id)
        {
            if (HttpContext.Session.GetInt32("UserId") is null)
            {
                Response.Redirect("/", false, true);
            }
            idEvent = Convert.ToInt32(id);
            try
            { 
              
                if (idEvent == 1)
                {
                    var q = db.Events
              .Where(c => c.EventType == "Conference").ToList();
                }
                else if (idEvent == 2)
                {
                    var q = db.Events
                                 .Where(c => c.EventType == "Wedding").ToList();

                }


                else if (idEvent == 3) 
                {

                    var q = db.Events
                .Where(c => c.EventType == "Concert").ToList();

                }
                 else
                    {
                        var q = db.Events
                .Where(c => c.EventType == "Concert"|| c.EventType == "Wedding" || c.EventType == "Conference").ToList();

                    }



            }
            catch (Exception ex) { 
            
            }
        }
    }
    
}
