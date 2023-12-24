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

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("UserId") is null)
            {
                return RedirectToPage("/Login");
            }
           
            var eventToDlete = db.Events.Where(item => item.EventID == id).Select(x => x);
            if (eventToDlete != null)
            {
                db.Events.Remove(eventToDlete.First());
                db.SaveChanges();
            }

            return RedirectToPage("/Events");

        }

       
    }
    }
