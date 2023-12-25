using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MainProject.Models;
using System.Data;

namespace MainProject.Pages
{
    public class FinanceModel : PageModel
    {
        private readonly Context db;

		// Dictionary Mapping Query name to the corrosponding value
		public Dictionary<string, double> Queries { get; set; }
		public Dictionary<int, double> TopRoomIncome { get; set; }
		public Dictionary<string, double> TopEventIncome { get; set; }

		public string? Error {  get; set; }

		public FinanceModel(Context db)
        {
            this.db = db;
            TopRoomIncome = new();
            TopEventIncome = new();

            // Financial Queries
            Queries = new()
                {
                    { "Room Income This Year", (from transaction in db.Transactions where transaction.TransactionTime.Year == DateTime.Now.Year select transaction.TransactionFee).Sum() },
                    { "Room Income Last Year", (from transaction in db.Transactions where transaction.TransactionTime.Year == DateTime.Now.Year - 1 select transaction.TransactionFee).Sum() },
                    { "Room Income This Month", (from transaction in db.Transactions where (transaction.TransactionTime.Year == DateTime.Now.Year && transaction.TransactionTime.Month == DateTime.Now.Month) select transaction.TransactionFee).Sum() },
                    { "Total Room Income", db.Transactions.Select(transaction => transaction.TransactionFee).Sum() },
                    { "Event Income This Year", (from hotelEvent in db.Events where hotelEvent.EventStart.Year == DateTime.Now.Year select hotelEvent.EventFee).Sum() },
                    { "Event Income Last Year", (from hotelEvent in db.Events where hotelEvent.EventStart.Year == DateTime.Now.Year - 1 select hotelEvent.EventFee).Sum() },
                    { "Event Income This Month", (from hotelEvent in db.Events where (hotelEvent.EventStart.Year == DateTime.Now.Year && hotelEvent.EventStart.Month == DateTime.Now.Month) select hotelEvent.EventFee).Sum() },
                    { "Total Event Income", db.Transactions.Select(hotelEvent => hotelEvent.TransactionFee).Sum() },
                    { "Total Employee Salaries", db.Employees.Select(employee => employee.EmployeeSalary).Sum() },
                    { "Average Employee Salary", db.Employees.Select(employee => employee.EmployeeSalary).Average() },
                    { "Minimum Employee Salary", db.Employees.Select(employee => employee.EmployeeSalary).Min() },
                    { "Number Of Employees", db.Employees.Count() },
                    { "Number Of Rooms", db.Rooms.Count() },
                    { "Number Of Occupied Rooms", (from booking in db.Bookings where (booking.CheckIn >= DateTime.Now && booking.CheckOut < DateTime.Now) select booking).Count()}
                };
            if (DateTime.Now.Month == 1) // If it's January in Year (Y), so the last month will be December Year (Y - 1)
            {
                Queries.Add("Room Income Last Month", (from transaction in db.Transactions where (transaction.TransactionTime.Year == DateTime.Now.Year - 1 && transaction.TransactionTime.Month == 12) select transaction.TransactionFee).Sum());
                Queries.Add("Event Income Last Month", (from hotelEvent in db.Events where (hotelEvent.EventStart.Year == DateTime.Now.Year - 1 && hotelEvent.EventStart.Month == 12) select hotelEvent.EventFee).Sum());
            }

            else
            {
                Queries.Add("Event Income Last Month", (from hotelEvent in db.Events where (hotelEvent.EventStart.Year == DateTime.Now.Year && hotelEvent.EventStart.Month == DateTime.Now.Month - 1) select hotelEvent.EventFee).Sum());
                Queries.Add("Room Income Last Month", (from transaction in db.Transactions where (transaction.TransactionTime.Year == DateTime.Now.Year && transaction.TransactionTime.Month == DateTime.Now.Month - 1) select transaction.TransactionFee).Sum());
            }

            Queries.Add("Month Room Income Change", ((Queries["Room Income This Month"] - Queries["Room Income Last Month"]) / Queries["Room Income Last Month"]) * 100);
            Queries.Add("Month Event Income Change", (Queries["Event Income This Month"] - Queries["Event Income Last Month"]) / Queries["Event Income Last Month"] * 100);
            Queries.Add("Year Room Income Change", (Queries["Room Income This Year"] - Queries["Room Income Last Year"]) / Queries["Room Income Last Year"] * 100.0);
            Queries.Add("Year Event Income Change", (Queries["Event Income This Year"] - Queries["Event Income Last Year"]) / Queries["Event Income Last Year"] * 100);
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserId") is null || (HttpContext.Session.GetString("UserType") != "manager"))
            {
                return RedirectToPage("/Login");
            }
         

            if (Queries is null)
            {
                Error = "Not Enough Data";
                return Page();
            }

			try
			{
				//top income
				//room

				var roomIncome = db.Transactions.GroupBy(t => t.TransactionRoom.RoomID);
				foreach (var room in roomIncome)
				{
					TopRoomIncome.TryAdd(room.Key, room.Sum(t => t.TransactionFee));
				}
				TopRoomIncome = TopRoomIncome.OrderByDescending(r => r.Value).Take(5).ToDictionary(r => r.Key, r => r.Value);
                //TopEventIncome = db.Events.Where(e => e.EventStart.Year == DateTime.Now.Year).OrderByDescending(e => e.EventFee).Take(5).ToDictionary(e => string.Format($"Event {e.EventID} - {e.EventName}"), e => e.EventFee);
                TopEventIncome = db.Events.OrderByDescending(e => e.EventFee).Take(5).ToDictionary(e => e.EventID.ToString(), e => e.EventFee);

			}
			catch
			{
				Error = "Not enough data";
			}
            return Page();
        }

        public IActionResult OnPost()
        {
            ChromePdfRenderer renderer = new();
            PdfDocument pdf = renderer.RenderHtmlAsPdf(GetHtml(Queries));


            return File(pdf.BinaryData, "application/pdf", DateTime.Now.ToString() + ".pdf");
        }


        public string GetHtml(Dictionary<string, double> queries)
        {


            string html = $"""
            <body style="font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif; text-align: center; align-items: center;">
            <p style="text-align: center; color: #d4af7a; font-size: 65px;">HarmoniStay</p>
            <h1 style="text-align: center"> Financial Report </h1>
            <h4 style="text-align: center"> {DateTime.Now} </h4>
            <table style="width: 100%; border-collapse: collapse; margin-top: 20px; border: 2px solid #d4af7a;">
                <thead style="background-color: #d4af7a">
                    <tr style="border: 2px solid #d4af7a">
                        <th style="text-align: center; padding: 6px; border: 2px solid #d4af7a;">Query</th> 
                        <th style="text-align: center; padding: 6px;">Value</th>
                    </tr>
                </thead>
            """;
            foreach (var query in queries)
            {
                html += $"""
                <tbody>
                    <tr text-align: center; border: 2px solid #d4af7a>
                        <td style="text-align: center; padding: 6px; border: 2px solid #d4af7a;">{query.Key}</td>
                        <td style="text-align: center; padding: 6px; border: 2px solid #d4af7a;">{query.Value}</td>
                    </tr>
                """;
            }
            html += $"""
            </tbody></table></body>
            """;


            return html;
        }
    }

}
