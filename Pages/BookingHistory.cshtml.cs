using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Pages
{
    public class BookingHistoryModel : PageModel
    {
        public bool Error = false;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string GuestId { get; set; }
        public string Room { get; set; }

        private readonly Context db;

        public BookingHistoryModel(Context db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/Login", false, true);
                return;
            }
        }
        public void OnPost()
        {
            try
            {
                this.GuestId = Request.Form["guestId"];
            }
            catch
            {
                Error = true;
            }
        }
    }
}
