using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;
using Microsoft.EntityFrameworkCore;


namespace MainProject.Pages
{
    public class BookingModel : PageModel
    {
        public bool Error { get; set; }
        public DateTime CheckIn { get; set;}
        public DateTime CheckOut { get; set; }
        public string GuestId { get; set; }
        public string? GuestName { get; set; }
        public string? GuestNationality { get; set; }
        public string? GuestPhoneNumber { get; set; }
        public string? Message { get; set; }
        public int SelectedRoomId { get; set; }
        public IList<Room> AvailableRooms { get; set; }
        private readonly Context db;

        public BookingModel(Context db)
        {
            this.db = db;
        }

        public void OnGet()
        {}
        
        public void OnPost()
        {
            try 
            {
                this.CheckIn = Convert.ToDateTime(Request.Form["checkin"]);
                this.CheckOut = Convert.ToDateTime(Request.Form["checkout"]);
                this.GuestId = Request.Form["guestId"];

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


            // Get all rooms
            var rooms = db.Rooms.ToList();

            // Get booked room IDs within the specified period
            var bookedRoomIds = db.Bookings
                .Where(x => this.CheckIn < x.CheckOut && this.CheckOut > x.CheckIn)
                .Select(x => x.BookingRoom.RoomID)
                .ToList();

            // Get available rooms
            AvailableRooms = rooms.Where(x => !bookedRoomIds.Contains(x.RoomID)).ToList();


            if (SelectedRoomId == 0)
            {
                Error = true;
                Message = "Please select a room.";
            }
            else
            {
                // Check if the selected room is still available (optional, depending on your business logic)
                var isRoomAvailable = db.Bookings
                    .All(booking =>
                        SelectedRoomId != booking.BookingRoom.RoomID ||
                        CheckIn >= booking.CheckOut ||
                        CheckOut <= booking.CheckIn);

                if (isRoomAvailable)
                {
                    Guest? guests= db.Guests.SingleOrDefault(x => x.GuestID == this.GuestId);

                    if (guests is null)
                    {
                        db.Guests.Add(new Guest() { GuestID = this.GuestId, GuestName = this.GuestName, GuestNationality = this.GuestNationality, GuestPhoneNumber = GuestPhoneNumber });
                    }

                    db.Bookings.Add(new Booking() { CheckIn = this.CheckIn, CheckOut = this.CheckOut, BookingRoom = db.Rooms.Find(SelectedRoomId), BookingGuest = guests });
                    db.SaveChanges();

                    Message = $"Booking successful for Room ID: {SelectedRoomId}";
                }
                else
                {
                    Error = true;
                    Message = "The selected room is not available for the specified period.";
                }
            }


            //var queryGuest = db.Guests.Where(x => x.GuestID == this.GuestId).Select(x => new { x.GuestName, x.GuestNationality, x.GuestPhoneNumber });
            //if (queryGuest is not null)
            //{
            //    foreach (var x in queryGuest)
            //    {
            //        this.GuestName = x.GuestName;
            //        this.GuestNationality = x.GuestNationality;
            //        this.GuestPhoneNumber = x.GuestPhoneNumber;
            //    }
            //}
        }
    }
}
