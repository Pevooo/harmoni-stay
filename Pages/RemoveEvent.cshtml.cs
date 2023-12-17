using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
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

        //public void OnPostSubmit(string id)
        //{
        //    var deletedCustomer = (from c in this.db.Events
        //                           where c.EventID == id
        //                           select c).FirstOrDefault();

        //    if (deletedCustomer != null)
        //    {
        //        this.db.Events.Remove(deletedCustomer);
        //        this.db.SaveChanges();
        //        ViewData["Message"] = "Customer record deleted.";
        //    }
        //    else
        //    {
        //        ViewData["Message"] = "Customer not found.";
        //    }
        public async Task<IActionResult> OnGet(string id)
        {
            if (HttpContext.Session.GetInt32("UserId") is null)
            {
                Response.Redirect("/", false, true);
            }
            var eventToDlete = db.Events.Where(item => item.EventID == id).Select(x => x);
            if (eventToDlete != null)
            {
                db.Events.Remove(eventToDlete.First());
                await db.SaveChangesAsync();
            }

            return RedirectToPage("/Events");

        }

        //public async Task<IActionResult> OnGet(int id)
        //{
        //    if (HttpContext.Session.GetString("UserId") is null)
        //    {
        //        Response.Redirect("/", false, true);
        //    }

        //    var eventToDelete = db.Events.FirstOrDefault(i => i.EventID == id);
        //    if (eventToDelete != null)
        //    {
        //        db.Events.Remove(eventToDelete);
        //        await db.SaveChangesAsync();
        //    }
        //    return RedirectToPage("/Events");
        //}
        //public async Task<IActionResult> OnPostDelete(int id)
        //{
        //    var eventToDelete = await db.Events.FindAsync(id);
        //    if (eventToDelete == null)
        //    {
        //        return NotFound();

        //    }
        //    db.Events.Remove(eventToDelete);
        //    await db.SaveChangesAsync();

        //    return RedirectToPage("/Events");
        //}

        //public void OnGet()
        //{
        //    products = db.Events.ToList();
        //}

        //public IActionResult OnGetDelete(int id)
        //{
        //    db.Remove(db.Events.Find(id));
        //    db.SaveChanges();
        //    return RedirectToPage("Index");
        //}
    }
    }
}