using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;
using Microsoft.EntityFrameworkCore;


namespace MainProject.Pages
{
    public class BookingModel : PageModel
    {
        public string Error { get; set; }
        public DateTime CheckIn { get; set;}
        public DateTime CheckOut { get; set; }
        public string GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestNationality { get; set; }
        public string GuestPhoneNumber { get; set; }
        public int SelectedRoomId { get; set; }
        public List<Room> HotelRooms { get; set; }
        public Guest HotelGuest { get; set; }
	    public double TransactionFee{ get; set;}
	    public string TransactionDescription{ get; set;}
        private readonly Context db;

        public BookingModel(Context db)
        {
            this.db = db;
            HotelRooms= new List<Room>();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
             return RedirectToPage("/Login");
                
            }
            this.SelectedRoomId = 0;
            HotelRooms = db.Rooms.ToList();
            return Page();
        }
        
        public void OnPost()
        {
            HotelRooms = db.Rooms.ToList();
            try 
            {
                this.CheckIn = DateTime.Parse(Request.Form["checkin"].ToString());
                this.CheckOut = DateTime.Parse(Request.Form["checkout"].ToString());
                this.GuestId = Request.Form["guestId"];
                this.GuestName = Request.Form["guestName"];
                this.GuestNationality = Request.Form["guestNationality"];
                this.GuestPhoneNumber = Request.Form["guestPhoneNumber"];
                this.SelectedRoomId = int.Parse(Request.Form["SelectedRoomId"]);
		        this.TransactionFee = double.Parse(Request.Form["transactionFee"]);
		        this.TransactionDescription = Request.Form["transactionDescription"];

                if (this.CheckOut <= this.CheckIn)
                {
                    TempData["Error"] = "Check your check-in and Check-out dates.";
                    Error = "true1";
                    return;

                }
                if (this.GuestName == "" || this.GuestNationality == ""  || this.GuestPhoneNumber == "")
                {
                    TempData["Error"] = "Check Guest Data.";
                    Error = "true2";
                    return;
                }
                if (this.TransactionFee <= 0 || this.TransactionDescription == "")
                {
                    TempData["Error"] = "Check Transaction Data.";
                    Error = "true3";
                    return;
                }
                if (this.SelectedRoomId == 0)
                {
                    TempData["Error"] = "Please select a room.";
                    Error = "true4";
                    return;
                }
                else
                {
                    //Checking room availability
                    var AvailableRoom = !db.Bookings.Any(booked => (this.SelectedRoomId == booked.BookingRoom.RoomID && this.CheckIn < booked.CheckOut && this.CheckOut > booked.CheckIn));
                    if (AvailableRoom)
                    {
                        //checking if the guest is new or not
                        var existingGuest = db.Guests.SingleOrDefault(guest => guest.GuestID == this.GuestId);

                        if (existingGuest is null)
                        {
                            this.HotelGuest = new Guest
                            {
                                GuestID = this.GuestId,
                                GuestName = this.GuestName,
                                GuestNationality = this.GuestNationality,
                                GuestPhoneNumber = this.GuestPhoneNumber
                            };
                            db.Guests.Add(this.HotelGuest);
                            db.SaveChanges();
                        }

                        var newBooking = new Booking() { CheckIn = this.CheckIn, CheckOut = this.CheckOut, BookingRoom = db.Rooms.Find(SelectedRoomId), BookingGuest = this.HotelGuest };
                        db.Bookings.Add(newBooking);
                        db.SaveChanges();

                        var newTransaction = new Transaction
                        {
                            TransactionDescription = $"Room {SelectedRoomId} has Booked. {this.TransactionDescription}",
                            TransactionFee = this.TransactionFee,
                            TransactionTime = DateTime.Now,
                            TransactionRoom = newBooking.BookingRoom
                        };

                        db.Transactions.Add(newTransaction);
                        db.SaveChanges();

                        TempData["Success"] = $"Booking successful for Room ID: {SelectedRoomId}";
                    }
                    else
                    {
                        TempData["Error"] = "The selected room is not available for the specified period.";
                        Error = "true5";
                        return;
                    }
                }
            } 
            catch 
            {
                TempData["Error"] = "oops,error";
                Error = "true";
                return;
            }

        }
    }
}
