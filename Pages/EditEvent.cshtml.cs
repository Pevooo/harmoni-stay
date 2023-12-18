using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Policy;

namespace MainProject.Pages
{
    public class EditEventModel : PageModel
    {

        private readonly Context db;
        public EditEventModel(Context db)
        {
            this.db = db;

        }
        public string EventName { get; set; }

        public string EventType { get; set; }
        public int Id { get; set; }
        public DateTime st { get; set; }
        public DateTime end { get; set; }

        public bool Error { get; set; }
       public void OnGet(int id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/", false, true);
            }
            var eventToEdit = db.Events.FirstOrDefault(x => x.EventID == id);

            EventName = eventToEdit.EventName;
            Id = eventToEdit.EventID;

            st = eventToEdit.EventStart;
            end = eventToEdit.EventEnd;

        }
        public void OnPost(int id)
        {
            try
            {
                var eventToEdit = db.Events.FirstOrDefault(x => x.EventID == id);

                EventName = Request.Form["EventName"];
                st = DateTime.Parse(Request.Form["startDate"]);
                end = DateTime.Parse(Request.Form["endDate"]);

                eventToEdit.EventName = EventName;
                eventToEdit.EventStart = st;
                eventToEdit.EventEnd = end;


                db.SaveChanges();
                Response.Redirect("/Events", false, true);
            }
            catch
            {
                Error = true;
                return;

            }

        }
    }
}