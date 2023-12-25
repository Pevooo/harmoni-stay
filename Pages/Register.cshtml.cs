using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MainProject.Models;
using Microsoft.IdentityModel.Tokens;

namespace MainProject.Pages
{
    public class RegisterModel : PageModel
    {

	    public bool Error { get; set; }

		public string Password { get; set; }

		public string UserId { get; set; }

		public string Type { get; set; }

		public byte[] Image { get; set; }

		public string? ImageURL { get; set; }

		private readonly Context db;

        public RegisterModel(Context db)
        {
			this.db = db;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is not null)
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }
		private bool check_Password (string pass)
		{
			int Capital = 0;
			for(int i=0;i<pass.Count();i++)
			{
				if (pass[i] >= 65 && pass[i] <= 91) Capital++;

            }
			return Capital > 3&&pass.Count()>=8;
		}
        public void OnPost()
        {

            try
			{
				UserId = Request.Form["userId"];
				Password = Request.Form["password"];
				Type = Request.Form["type"];
				if (Type.ToLower() != "manager" && Type.ToLower() != "receptionist")
				{
					Error = true;
					return;
				}
            }
			catch
			{
				Error = true;
				return;
			}

			if (UserId.IsNullOrEmpty() || Password.IsNullOrEmpty() || Type.IsNullOrEmpty()) 
			{
				Error = true;
				return;
			}



            Account? query = db.Accounts.SingleOrDefault(account => account.AccountEmployee.EmployeeID == UserId);

			if (query is not null)
			{
				Error = true;
			}else if (!check_Password(Password)) { 
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

				
				db.Accounts.Add(new Account() {  AccountEmployee = employee, Password = BCrypt.Net.BCrypt.HashPassword(Password), Type = this.Type});
				db.SaveChanges();

				// Saving User info in Session and Globals
				HttpContext.Session.SetString("UserId", UserId);
                HttpContext.Session.SetString("UserType", Type.ToLower());
                Response.Redirect("/Login", false, true);
			}
		}    
    }
}
