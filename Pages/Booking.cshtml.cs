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
                this.GuestName = Request.Form["guestName"];
                this.GuestNationality = Request.Form["guestNationality"];
                this.GuestPhoneNumber = Request.Form["guestPhoneNumber"];
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


            if (SelectedRoomId == 0)
            {
                //Error = true;
                Message = "Please select a room.";
            }
            else
            {
		var AvailableRoom = ! db.Bookings.Any(x => this.SelectedRoomId == x.BookingRoom.RoomID && this.CheckIn < x.CheckOut && this.CheckOut > x.CheckIn);
                if (AvailableRoom)
                {
                    var existingGuest = db.Guests.Find(this.GuestId);
                    //Guest? guests= db.Guests.SingleOrDefault(x => x.GuestID == this.GuestId);

                    if (existingGuest == null)
                    {
                        existingGuest = new Guest
                        {
                            GuestID = this.GuestId,
                            GuestName = this.GuestName,
                            GuestNationality = this.GuestNationality,
                            GuestPhoneNumber = this.GuestPhoneNumber
                        };
                        db.Guests.Add(existingGuest);
                        db.SaveChanges();
                    }

                    var newBooking = new Booking() { CheckIn = this.CheckIn, CheckOut = this.CheckOut, BookingRoom = db.Rooms.Find(SelectedRoomId), BookingGuest = existingGuest };
                    db.Bookings.Add(newBooking);
                    db.SaveChanges();

                    var newTransaction = new Transaction
                    {
                        TransactionDescription = $"Room {SelectedRoomId} has Booked",
                        TransactionFee = 10.0,
                        TransactionTime = DateTime.Now,
                        TransactionRoom = newBooking.BookingRoom
                    };

                    db.Transactions.Add(newTransaction);
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
