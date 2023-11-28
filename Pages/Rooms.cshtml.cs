using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MainProject.Models;

namespace MainProject.Pages
{
    public class RoomsModel : PageModel
    {

        public RoomsModel()
        {
            db = new();
        }

        private readonly Context db;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public bool Error { get; set; }
        public string? Message { get; set; }

        public void OnGet()
        {
  
        }

        public void OnPost()
        {
            try
            {
                StartDate = DateTime.Parse(Request.Form["startDate"]);
                EndDate = DateTime.Parse(Request.Form["endDate"]);
                RoomId = Convert.ToInt32(Request.Form["roomId"]);
            }
            catch
            {
                Error = true;
                return;
            }

            bool roomExists = db.Rooms.SingleOrDefault(room => room.RoomID == RoomId) is not null;
            if (!roomExists)
            {
                Error = true;
                return;
            }

            var bookings = (from booking in db.Bookings where (booking.BookingRoom.RoomID == RoomId && StartDate <= booking.CheckOut && EndDate >= booking.CheckIn) select booking).ToList();

            if (bookings.Count == 0)
            {
                // Free
                Message = "Room is Free";
            }
            else
            {
                // Occupied
                Message = "Room is Occupied";
            }
        }

    }
}
