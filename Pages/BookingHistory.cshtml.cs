using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Pages
{
    public class BookingHistoryModel : PageModel
    {
        public string Error { get; set; }
        public string GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestNationality { get; set; }
        public string GuestPhoneNumber { get; set; }
	    public IList<Booking> GuestHistory { get; set; }

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
                if (this.GuestId == "")
                {
                    TempData["Error"] = "Not Found";
                    Error = "true";
                }
                //guest data
                var guest = db.Guests.SingleOrDefault(guest => guest.GuestID == this.GuestId);
                if (guest != null)
                {
                    this.GuestName = guest.GuestName;
                    this.GuestNationality = guest.GuestNationality;
                    this.GuestPhoneNumber = guest.GuestPhoneNumber;
                }
                //history // Include related room information
                this.GuestHistory = db.Bookings.Include(room => room.BookingRoom).Where(booking => booking.BookingGuest.GuestID == this.GuestId).ToList();
            }
            catch
            {
                TempData["Error"] = "Not Found";
                Error = "true";
            }
        }
    }
}
