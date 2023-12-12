using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MainProject.Models;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MainProject.Pages
{


    

    public class ChatbotModel : PageModel
    {
        public string? Prompt { get; set; }
        public string Output { get; set; }
        public string? Error { get; set; }

        private readonly Context db;

        public ChatbotModel(Context db)
        {
            this.db = db;
        }

        public void OnGet()
        {

        }


        public void OnPost() 
        {
            Prompt = Request.Form["prompt"];
            if (Prompt.Length <= 1)
            {
                Output = Globals.ChatBot.Query(Prompt);
                return;
            }

            try
            {
                OnlineChatbot onlineChatbot = new("text-davinci-003", db);
                Output = "";
                foreach(string choice in onlineChatbot.Query(Prompt))
                {
                    Output += choice + '\n';
                }

            }
            catch
            {
                Error = "Internet Connection is required for the Chatbot to give accurate responses";
                Output = Globals.ChatBot.Query(Prompt);
            }



            
        }
    }
}
