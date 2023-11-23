using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;


namespace MainProject.Pages
{
    public class RegisterModel : PageModel
    {

	    public bool Error { get; set; }

		public string Password { get; set; }

		public int UserId { get; set; }

		private readonly Context db;

        public RegisterModel()
        {
			db = new Context();
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


			Account? query = db.Accounts.SingleOrDefault(account => account.AccountEmployee.EmployeeID == UserId);

			if (query is not null)
			{
				Error = true;
			}
			else
			{
				// Updating the database
				Employee? employee = db.Employees.SingleOrDefault(emp => emp.EmployeeID == UserId);
				
				if (employee is null)
				{
					Error = true;
					return;
				}
				
				db.Accounts.Add(new Account() {  AccountEmployee = employee, Password = BCrypt.Net.BCrypt.HashPassword(Password), Type = "Manager" });
				db.SaveChanges();

				// Saving User info in Session and Globals
				HttpContext.Session.SetInt32("UserId", (int)UserId);
				Globals.UserId = UserId;
				Response.Redirect("/", false, true);
			}
		}    
    }
}
