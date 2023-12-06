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

		public string Type { get; set; }

		public byte[] Image { get; set; }

		public string? ImageURL {  get; set; }

		private readonly Context db;

        public RegisterModel(Context db)
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
            MemoryStream memoryStream = new MemoryStream();
            try
			{
				UserId = Convert.ToInt32(Request.Form["userId"]);
				Password = Request.Form["password"];
				Type = Request.Form["type"];
				Request.Form.Files.First().CopyTo(memoryStream);
				
            }
			catch
			{
				Error = true;
				return;
			}
            ImageURL = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(memoryStream.ToArray()));

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
				
				db.Accounts.Add(new Account() {  AccountEmployee = employee, Password = BCrypt.Net.BCrypt.HashPassword(Password), Type = this.Type, Image = memoryStream.ToArray()});
				db.SaveChanges();

				// Saving User info in Session and Globals
				//HttpContext.Session.SetInt32("UserId", (int)UserId);
				//Globals.UserId = UserId;
				Response.Redirect("/Login", false, true);
			}
		}    
    }
}
