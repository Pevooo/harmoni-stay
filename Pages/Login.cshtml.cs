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

        private Context db;
        public int UserId { get; set; }
        public string Password {  get; set; }
        public bool Error { get; set; }

        public LoginModel(Context db)
        {
            this.db = db;
        }


        public void OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") is not null)
            {
                Response.Redirect("/", false, true);
            }
        }


        public void OnPost()
        {

            try
            {
				UserId = Convert.ToInt32(Request.Form["userId"]);
				Password = Request.Form["password"];
			}
            catch
            {
                Error = true;
                return;
            }


			// Checking the existance of the account with the correct password
			var query = db.Accounts.SingleOrDefault(account => account.AccountEmployee.EmployeeID == UserId);

            if (query is null || !BCrypt.Net.BCrypt.Verify(Password, query.Password)) // If no account is found with the given credentials or password is wrong
            {
                Error = true;
            }
            else
            {
                // Saving User info in Session and Globals
                HttpContext.Session.SetInt32("UserId", (int)UserId);
                var emp=db.Employees.SingleOrDefault(e => e.EmployeeID == UserId);
                Globals.UserId = UserId;
                Globals.UserName = emp.EmployeeName;
                Response.Redirect("/", false, true);      
            }

        }
    }
}
