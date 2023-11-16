using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MainProject.Models;
using Microsoft.AspNetCore.Http;

namespace MainProject.Pages
{
    public class LoginModel : PageModel
    {
      
        private readonly ILogger<LoginModel> _logger;

        public int userId;
        public string? password;
        public bool Error { get; set; }

        public string? name;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
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
            password = Request.Form["password"];
            try
            {
				userId = Convert.ToInt32(Request.Form["userId"]);
			}
            catch
            {
                Error = true;
                return;
            }
            

            List<Account> accounts = new Context().Accounts.ToList();

            // Checking the existance of the account with the correct password
            var query = (from account in accounts where account.AccountID == userId && Utility.CheckHash(password, account.Password) select account).ToList();

            if (query.Count == 0) // If no account is found with the given credentials
            {
                Error = true;
            }
            else
            {
                // Saving User info in Session and Globals
                HttpContext.Session.SetInt32("UserId", userId);
                Globals.UserId = userId;
                Response.Redirect("/", false, true);      
            }
        }
    }
}
