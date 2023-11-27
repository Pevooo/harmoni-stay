using MainProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainProject.Pages
{
    public class IndexModel : PageModel
    {
        //[BindProperty]
        //public Booking booking { get; set; }
        public bool Error { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestNationality {  get; set; }
        public string GuestPhoneNumber { get; set; }
        private readonly Context db;
        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public void OnGet(int id)
        {
            GuestId = id;
            var queryGuest = db.Guests.Where(x => x.GuestID == GuestId).Select(x=>new{ x.GuestName,x.GuestNationality,x.GuestPhoneNumber});
            if (queryGuest is not null)
            {
                foreach(var x in queryGuest)
                {
                    GuestName = x.GuestName;
                    GuestNationality = x.GuestNationality;
                    GuestPhoneNumber = x.GuestPhoneNumber;
                }
            }
            //Session = HttpContext.Session;
            //UserId = HttpContext.Session.GetInt32("userID");

        }

        public void OnPost()
        {
            // Updating the database
				Guest? guest = db.Guests.SingleOrDefault(g => g.GuestID == GuestId);
				
				if (guest is null)
				{
					Error = true;
					return;
				}
				
				db.Guests.Add(new Guest() {  GuestID = GuestId, GuestName = GuestName, GuestNationality = GuestNationality, GuestPhoneNumber = GuestPhoneNumber });
				db.SaveChanges();
            //UserId = HttpContext.Session.GetInt32("userID");
        }
    }
}