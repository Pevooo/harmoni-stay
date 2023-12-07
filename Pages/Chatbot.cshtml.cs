using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainProject.Pages
{


    

    public class ChatbotModel : PageModel
    {
        public string? Prompt { get; set; }
        public string Output { get; set; }
        public void OnGet()
        {

        }


        public void OnPost() 
        {
            Prompt = Request.Form["prompt"];
            Output = Globals.ChatBot.Query(Prompt);
        }
    }
}
