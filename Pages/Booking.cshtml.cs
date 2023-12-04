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
        public string? GuestName { get; set; }
        public string? GuestNationality { get; set; }
        public string? GuestPhoneNumber { get; set; }
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
                this.CheckIn = Convert.ToDateTime(Request.Form["checkin"]);
                this.CheckOut = Convert.ToDateTime(Request.Form["checkout"]);
                this.GuestId = Convert.ToInt32(Request.Form["guestId"]);

            } 
            catch 
            {
                Error = true;
                return;
            }

            if (this.CheckOut <= this.CheckIn)
            {
                Error = true;
                return;
            }
            //var RoomTypes
            var queryGuest = db.Guests.Where(x => x.GuestID == this.GuestId).Select(x => new { x.GuestName, x.GuestNationality, x.GuestPhoneNumber });
            if (queryGuest is not null)
            {
                foreach (var x in queryGuest)
                {
                    this.GuestName = x.GuestName;
                    this.GuestNationality = x.GuestNationality;
                    this.GuestPhoneNumber = x.GuestPhoneNumber;
                }
            }


            // Updating the database
            Guest? guest = db.Guests.SingleOrDefault(g => g.GuestID == this.GuestId);

            if (guest is null)
            {
                db.Guests.Add(new Guest() { GuestID = this.GuestId, GuestName = this.GuestName, GuestNationality = this.GuestNationality, GuestPhoneNumber = GuestPhoneNumber });
                db.SaveChanges();
            }

            
        }

        // is null) //new guest
        //    {
                
        //    }
        //    else
        //    {

        //    }
        //    return null;
        //}
    }
}
