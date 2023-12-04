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

		public FinanceModel(Context db)
        {
            this.db = db;
        }
        public void OnGet()
        {
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
				{ "Total Employee Salaris", db.Employees.Select(employee => employee.EmployeeSalary).Sum() },
				{ "Average Employee Salary", db.Employees.Select(employee => employee.EmployeeSalary).Average() },
				{ "Minimum Employee Salary", db.Employees.Select(employee => employee.EmployeeSalary).Min() },

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
			Queries.Add("Year Event Income Change",  (Queries["Event Income This Year"] - Queries["Event Income Last Year"]) / Queries["Event Income Last Year"] * 100);


        }
    }
}
