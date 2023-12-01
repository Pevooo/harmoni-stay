using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;

namespace MainProject.Pages
{
    public class BookingModel : PageModel
    {
        //[BindProperty]
        //public Booking booking { get; set; }
        public bool Error { get; set; }
        public DateTime CheckIn { get; set;}
        public DateTime CheckOut { get; set; }
        public int GuestId { get; set; }
        private readonly Context db;

        public BookingModel(Context db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            //booking = new Booking();
            //date = DateOnly.FromDateTime(DateTime.Now);
        }
        public void OnPost()
        {
            try 
            {
                CheckIn = Convert.ToDateTime(Request.Form["checkin"]);
                CheckOut = Convert.ToDateTime(Request.Form["checkout"]);
                GuestId = Convert.ToInt32(Request.Form["guestId"]);

            } 
            catch 
            {
                Error = true;
                return;
            }

            if(CheckIn >= CheckOut)
            {
                Error = true;
                return;
            }

            
            // Saving User info in Session and Globals
           // HttpContext.Session.SetInt32("GuestId", (int)GuestId);
            //Globals.UserId = UserId;
            //Response.Redirect("/Index", false, true);
             //return Page();
        }

        //private Booking reservation(DateTime checkin, DateTime checkout, int guestId, string roomType)
        //{
        //    var queryGuest = db.Bookings.Where(x => x.BookingGuest.GuestID == guestId);
        //    // && account.RoomType == roomType);
        //    if (queryGuest is null) //new guest
        //    {
                
        //    }
        //    else
        //    {

        //    }
        //    return null;
        //}
    }
}
