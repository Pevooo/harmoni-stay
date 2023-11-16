using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace MainProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public int? UserId;

        public static ISession Session { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Session = HttpContext.Session;
            UserId = HttpContext.Session.GetInt32("userID");
        }

        public void OnPost()
        {
            UserId = HttpContext.Session.GetInt32("userID");
        }
    }
}