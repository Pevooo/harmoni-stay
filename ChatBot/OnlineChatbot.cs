using Azure.AI.OpenAI;
using MainProject.Models;
public class OnlineChatbot
{
    public string Model { get; set; }
    public OpenAIClient Client { get; set; }

    private Context db { get; set; }

    public OnlineChatbot(string model, Context db)
    {
        Client = new("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");
        Model = model;
        this.db = db;

    }

    public List<string> Query(string query)
    {
        var response = Client.GetCompletions(new CompletionsOptions
        {
            DeploymentName = Model,
            Prompts = { GenerateQuestionText(query) },
            MaxTokens = 30
        });

        List<string> answer = new();
        foreach (Choice choice in response.Value.Choices)
        {
           answer.Add(choice.Text);
        }

        return answer;
    }

    public string GenerateQuestionText(string query)
    {

        return
        $"""
            Act like a chatbot in a hotel management system called HarmoniStay
            When asked for a page, provide the link only
            Number of Rooms: {db.Rooms.Count()},
            Number of Employees: {db.Employees.Count()},
            Number of Events Held: {db.Events.Count()},
            Number of Facilities: {db.Facilities.Count()},
            Number of Free Rooms: {(from booking in db.Bookings where (booking.CheckIn >= DateTime.Now && booking.CheckOut < DateTime.Now) select booking).Count()},
            Number of Occupied Rooms: {db.Rooms.Count() - (from booking in db.Bookings where (booking.CheckIn >= DateTime.Now && booking.CheckOut < DateTime.Now) select booking).Count()},
            To login <a href="/Login">here</a>
            You must contact an admin to register
            To logout click <a href="/Logout">here</a>
            To see financial details click <a href="/Finance">here</a>
            To see Facilities details click <a href="/Facilities">here</a>
            To see Booking details click <a href="/Booking">here</a>
            To see Event details click <a href="/Events">here</a>
            To see Staff details click <a href="/Staff">here</a>

            According to this context, respond to this message: {query}
            """;
    }
}