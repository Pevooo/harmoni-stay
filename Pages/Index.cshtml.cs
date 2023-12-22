using MainProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Context db;

        public IndexModel(Context db)
        {
            this.db = db;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
    }
}