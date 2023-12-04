using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MainProject.Models;

namespace MainProject.Pages
{
    public class FinanceModel : PageModel
    {
        private readonly Context db;

		// Dictionary Mapping Query name to the corrosponding value
		public Dictionary<string, double> Queries { get; set; }

		public int MonthRoomIncomeChange { get; set; }
		public int YearRoomIncomeChange { get; set; }
		public int MonthEventIncomeChange { get; set; }
		public int YearEventIncomeChange { get; set; }
		public FinanceModel(Context db)
        {
            this.db = db;
        }
        public void OnGet()
        {
			// Financial Queries
			Queries = new()
			{
				{ "RoomIncomeThisYear", (from transaction in db.Transactions where transaction.TransactionTime.Year == DateTime.Now.Year select transaction.TransactionFee).Sum() },
				{ "RoomIncomeLastYear", (from transaction in db.Transactions where transaction.TransactionTime.Year == DateTime.Now.Year - 1 select transaction.TransactionFee).Sum() },
				{ "RoomIncomeThisMonth", (from transaction in db.Transactions where (transaction.TransactionTime.Year == DateTime.Now.Year && transaction.TransactionTime.Month == DateTime.Now.Month) select transaction.TransactionFee).Sum() },
				{ "TotalRoomIncome", db.Transactions.Select(transaction => transaction.TransactionFee).Sum() },
				{ "EventIncomeThisYear", (from hotelEvent in db.Events where hotelEvent.EventStart.Year == DateTime.Now.Year select hotelEvent.EventFee).Sum() },
				{ "EventIncomeLastYear", (from hotelEvent in db.Events where hotelEvent.EventStart.Year == DateTime.Now.Year - 1 select hotelEvent.EventFee).Sum() },
				{ "EventIncomeThisMonth", (from hotelEvent in db.Events where (hotelEvent.EventStart.Year == DateTime.Now.Year && hotelEvent.EventStart.Month == DateTime.Now.Month) select hotelEvent.EventFee).Sum() },
				{ "TotalEventIncome", db.Transactions.Select(hotelEvent => hotelEvent.TransactionFee).Sum() }
			};
			if (DateTime.Now.Month == 1) // If it's January in Year (Y), so the last month will be December Year (Y - 1)
            {
				Queries.Add("RoomIncomeLastMonth", (from transaction in db.Transactions where (transaction.TransactionTime.Year == DateTime.Now.Year - 1 && transaction.TransactionTime.Month == 12) select transaction.TransactionFee).Sum());
				Queries.Add("EventIncomeLastMonth", (from hotelEvent in db.Events where (hotelEvent.EventStart.Year == DateTime.Now.Year - 1 && hotelEvent.EventStart.Month == 12) select hotelEvent.EventFee).Sum());
			}

            else
            {
				Queries.Add("EventIncomeLastMonth", (from hotelEvent in db.Events where (hotelEvent.EventStart.Year == DateTime.Now.Year && hotelEvent.EventStart.Month == DateTime.Now.Month - 1) select hotelEvent.EventFee).Sum());
				Queries.Add("RoomIncomeLastMonth", (from transaction in db.Transactions where (transaction.TransactionTime.Year == DateTime.Now.Year && transaction.TransactionTime.Month == DateTime.Now.Month - 1) select transaction.TransactionFee).Sum());
			}

			MonthRoomIncomeChange = (int)Math.Ceiling((Queries["RoomIncomeThisMonth"] - Queries["RoomIncomeLastMonth"]) / Queries["RoomIncomeLastMonth"]) * 100;
			MonthEventIncomeChange = (int)Math.Ceiling((Queries["EventIncomeThisMonth"] - Queries["EventIncomeLastMonth"]) / Queries["EventIncomeLastMonth"]) * 100;
			YearRoomIncomeChange = (int)Math.Ceiling((Queries["RoomIncomeThisYear"] - Queries["RoomIncomeLastYear"]) / Queries["RoomIncomeLastYear"]) * 100;
			YearEventIncomeChange = (int)Math.Ceiling((Queries["EventIncomeThisYear"] - Queries["EventIncomeLastYear"]) / Queries["EventIncomeLastYear"]) * 100;

		}
    }
}
