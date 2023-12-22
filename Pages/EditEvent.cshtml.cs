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
        public int EventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Message { get; set; }
        public bool Error { get; set; }
        public void OnGet(int id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/", false, true);
                return;
            }
            var eventToEdit = db.Events.FirstOrDefault(x => x.EventID == id);

            EventName = eventToEdit.EventName;
            EventId = eventToEdit.EventID;
            EventType = eventToEdit.EventType;
            StartDate = eventToEdit.EventStart;
            EndDate = eventToEdit.EventEnd;

        }
        public IActionResult OnPost(int id)
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
                }
            }
            catch
            {
                Error = true;
            }

            return RedirectToPage("/Events");

        }
    }
}