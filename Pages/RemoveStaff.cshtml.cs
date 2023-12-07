using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MainProject.Pages
{
    public class RemoveStaff : PageModel
    {
        public readonly Context db;
        public RemoveStaff(Context db)
        {
            this.db = db;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var emp=db.Employees.Where(item=>item.EmployeeID==id).Select(x=>x);
            if (emp != null)
            {
                db.Employees.Remove(emp.First());
                await db.SaveChangesAsync();
            }
            
            return RedirectToPage("/Staff");

        }
    }
}
