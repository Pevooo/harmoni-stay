using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.Intrinsics.Arm;

namespace MainProject.Pages
{
    public class RemoveEventModel : PageModel
    {
        private readonly Context db;
        public RemoveEventModel(Context db)
        {
            this.db = db;

        }
        public async Task<IActionResult> OnGet(int id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                Response.Redirect("/", false, true);
            }
          
            var item = db.Events.FirstOrDefault(i => i.EventID == id);
            if (item != null)
            {
                db.Events.Remove(item);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("/EventView"); 
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var eventBook = await db.Events.FindAsync(id);
            if (eventBook == null)
            {
                return NotFound();

            }
            db.Events.Remove(eventBook);
            await db.SaveChangesAsync();

            return RedirectToPage("/EventView");
        }

    }
}
