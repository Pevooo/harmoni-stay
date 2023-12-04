using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.Intrinsics.Arm;

namespace MainProject.Pages
{
    public class RemoveModel : PageModel
    {
        private readonly Context db;
        public RemoveModel(Context db)
        {
            this.db = db;
        
        }
        public async Task<IActionResult> OnGet(int id)
        {
            // Implement code to delete the data with the specified ID
            // Example:
            var item = db.Facilities.FirstOrDefault(i => i.FacilityID == id);
            if (item != null)
            {
                db.Facilities.Remove(item);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("/Booking"); // Redirect to the desired page after deletion
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await db.Facilities.FindAsync(id);
            if (book == null)
            {
                return NotFound();

            }
            db.Facilities.Remove(book);
            await db.SaveChangesAsync();

            return RedirectToPage("/Rooms");
        }
        //public async Task<IActionResult> OnDeleteAsync(int id) 
        //{ 
        //    // Implement code to delete the data with the specified ID
        //    // Example:
        //    var item = db.Facilities.FirstOrDefault(i => i.FacilityID == id);
        //    if (item != null)
        //    {
        //        db.Facilities.Remove(item);
        //        db.SaveChanges();
        //    }

        //    return RedirectToPage("/Booking"); // Redirect to the desired page after deletion
        //}
    }
}
