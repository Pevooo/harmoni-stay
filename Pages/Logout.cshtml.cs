using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace MainProject.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
            Globals.UserId = null;
            Response.Redirect("/", false, true);
        }

        public void OnPost()
        {
            HttpContext.Session.Clear();
            Globals.UserId = null;
            Response.Redirect("/", false, true);
        }
    }
}
