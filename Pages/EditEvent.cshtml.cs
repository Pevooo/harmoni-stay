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
        public string EventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Message { get; set; }
        public bool Error { get; set; }
        public void OnGet(string id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/", false, true);
            }
            var eventToEdit = db.Events.FirstOrDefault(x => x.EventID == id);

            EventName = eventToEdit.EventName;
            EventId = eventToEdit.EventID;
            EventType = eventToEdit.EventType;
            StartDate = eventToEdit.EventStart;
            EndDate = eventToEdit.EventEnd;

        }
        public void OnPost(string id)
        {
            try
            {
                
                    var eventToEdit = db.Events.FirstOrDefault(x => x.EventID == id);

                    EventName = Request.Form["EventName"];
                    StartDate = DateTime.Parse(Request.Form["startDate"]);
                    EndDate = DateTime.Parse(Request.Form["endDate"]);
                    EventType = Request.Form["EventType"];

                if (EndDate < StartDate)
                {
                    Error = true;
                    Message = "Invalid Dates";

                }
                else
                {

                    eventToEdit.EventName = EventName;
                    eventToEdit.EventStart = StartDate;
                    eventToEdit.EventEnd = EndDate;
                    eventToEdit.EventType = EventType;
                    eventToEdit.EventID = id;
                    db.SaveChanges();
                    Response.Redirect("/EditEvent", false, true);
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