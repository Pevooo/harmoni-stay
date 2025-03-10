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
            if (HttpContext.Session.GetString("UserId") is null || (HttpContext.Session.GetString("UserType") != "manager"))
            {
                return RedirectToPage("/Login");
            }
            // Implement code to delete the data with the specified ID
            // Example:
            var item = db.Facilities.FirstOrDefault(i => i.FacilityID == id);
            if (item != null)
            {
                db.Facilities.Remove(item);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("/Facilities"); // Redirect to the desired page after deletion
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
        
    }
}
