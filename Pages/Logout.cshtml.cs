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
            Response.Redirect("/", false, true);
        }

        public void OnPost()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/", false, true);
        }
    }
}
