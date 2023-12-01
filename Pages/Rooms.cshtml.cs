using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MainProject.Models;

namespace MainProject.Pages
{
    public class RoomsModel : PageModel
    {

        public RoomsModel(Context db)
        {
            this.db = db;
        }

        private readonly Context db;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public bool Error { get; set; }
        public string? Message { get; set; }
        public Dictionary<int, List<bool>> Days { get; set; }
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
            if(EndDate<=StartDate)
            {
                Error=true;
                return;
            }

            var roomIds = (from room in db.Rooms select room.RoomID).ToList();

            int roomRangeStart = Math.Max(0, roomIds.IndexOf(RoomId) - 5);
            int roomRangeEnd = Math.Min(roomIds.Count(), roomIds.IndexOf(RoomId) + 5);
      
            Message = "Room is Free";

            Days = new();

            for (int i = roomRangeStart; i <= roomRangeEnd; i++)
            {
                int roomId = roomIds[i];
                Days.Add(roomId, new());
                for (DateTime dt = StartDate; dt <= EndDate; dt = dt.AddDays(1))
                {              
                    var occupied = (from booking in db.Bookings where (booking.BookingRoom.RoomID == roomId && dt < booking.CheckOut && dt >= booking.CheckIn) select booking).Count() != 0;
                    Days[roomId].Add(occupied);
                }
            }


        }

    }
}
