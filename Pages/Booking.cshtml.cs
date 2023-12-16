using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;
using Microsoft.EntityFrameworkCore;


namespace MainProject.Pages
{
    public class BookingModel : PageModel
    {
        public bool Error = false;
        public DateTime CheckIn { get; set;}
        public DateTime CheckOut { get; set; }
        public string GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestNationality { get; set; }
        public string GuestPhoneNumber { get; set; }
        public string Message { get; set; }
        public int SelectedRoomId { get; set; }
        public List<Room> Rooms { get; set; }
        public Guest g1 { get; set; }
        private readonly Context db;

        public BookingModel(Context db)
        {
            this.db = db;
            Rooms= new List<Room>();
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/Login", false, true);
                return;
            }
            Rooms = db.Rooms.ToList();

        }
        
        public void OnPost()
        {
            Rooms = db.Rooms.ToList();
            try 
            {
                this.CheckIn = DateTime.Parse(Request.Form["checkin"].ToString());
                this.CheckOut = DateTime.Parse(Request.Form["checkout"].ToString());
                this.GuestId = Request.Form["guestId"];
                this.GuestName = Request.Form["guestName"];
                this.GuestNationality = Request.Form["guestNationality"];
                this.GuestPhoneNumber = Request.Form["guestPhoneNumber"];
                this.SelectedRoomId = int.Parse(Request.Form["SelectedRoomId"]);
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
          


            if (SelectedRoomId == null)
            {
                //Error = true;
                Message = "Please select a room.";
            }
            else
            {
		        var AvailableRoom = ! db.Bookings.Any(x => (this.SelectedRoomId == x.BookingRoom.RoomID && this.CheckIn < x.CheckOut && this.CheckOut > x.CheckIn));
                if (AvailableRoom)
                {
                    var existingGuest = db.Guests.SingleOrDefault(x => x.GuestID == this.GuestId);
                    //Guest? guests= db.Guests.SingleOrDefault(x => x.GuestID == this.GuestId);

                    if (existingGuest is null)
                    {
                        g1= new Guest
                        {
                            GuestID = this.GuestId,
                            GuestName = this.GuestName,
                            GuestNationality = this.GuestNationality,
                            GuestPhoneNumber = this.GuestPhoneNumber
                        };
                        db.Guests.Add(g1);
                        db.SaveChanges();
                    }

                    var newBooking = new Booking() { CheckIn = this.CheckIn, CheckOut = this.CheckOut, BookingRoom = db.Rooms.Find(SelectedRoomId), BookingGuest = g1 };
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
