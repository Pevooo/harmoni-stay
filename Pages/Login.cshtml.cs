using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Azure.Identity;
using System.Runtime.Intrinsics.Arm;

namespace MainProject.Pages
{
    public class LoginModel : PageModel
    {

        private readonly Context db;
        public string UserId { get; set; }
        public string Password {  get; set; }
        public string? Error { get; set; }

        public LoginModel(Context db)
        {
            this.db = db;
            Error = null;
        }


        public void OnGet()
        {
            Error = null;
            if (HttpContext.Session.GetString("UserId") is not null)
            {
                Response.Redirect("/", false, true);
            }
        }


        public IActionResult OnPost()
        {
            Error = null;
            try
            {
				UserId = Request.Form["userId"];
				Password = Request.Form["password"];
			}
            catch
            {
                Error = "true";
                TempData["Error"] = "Check Your Inputs ";
                return Page();
            }


			// Checking the existance of the account with the correct password
			var query = db.Accounts.SingleOrDefault(account => account.AccountEmployee.EmployeeID == UserId);

            if (query is null || !BCrypt.Net.BCrypt.Verify(Password, query.Password)) // If no account is found with the given credentials or password is wrong
            {
                Error = "true";
                TempData["Error"] = "Check Your Inputs ";
            }
            else
            {
                // Saving User info in Session and Globals
                HttpContext.Session.SetString("UserId", UserId);
                HttpContext.Session.SetString("UserType", query.Type.ToLower());

                Error = "false";
                var emp = db.Employees.SingleOrDefault(e => e.EmployeeID == UserId);
                TempData["Success"] = "Login Successfully";
                return RedirectToPage("/Index");      
            }
            return Page();

        }
    }
}
