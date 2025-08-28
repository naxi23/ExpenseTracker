using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.MVC.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ExpenseDBContext _context;

        public ReportsController(ExpenseDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var expenses = _context.Expenses.Include(e => e.SubCategory).ThenInclude(c => c.Category).ToList();
            List<ExpenseSummaryModel> model = Convert_ExpenseListToExpenseSummaryModelList(expenses);
            return View(model);
        }

        [HttpPost]
        public IActionResult GetExpenseSummary([FromBody] ExpenseFilterViewModel model)
        {
            
            var expenses = new List<Expense>();
            if (model.Flag == 0 && DateTime.TryParse(model.StartDate, out DateTime sDate) && DateTime.TryParse(model.EndDate, out DateTime eDate))
            {
                expenses = _context.Expenses.Include(e => e.SubCategory).ThenInclude(c => c.Category)
                    .Where(e => e.CreatedDate >= sDate && e.CreatedDate <= eDate)
                    .ToList();
            }
            else if (model.Flag == 1 && int.TryParse(model.Month, out int m) && int.TryParse(model.Year, out int y))
            {
                expenses = _context.Expenses.Include(e => e.SubCategory).ThenInclude(c => c.Category)
                    .Where(e => e.CreatedDate.Month == m && e.CreatedDate.Year == y)
                    .ToList();
            }
            List<ExpenseSummaryModel> res = Convert_ExpenseListToExpenseSummaryModelList(expenses);
            return PartialView("_ExpenseSummaryPartial", res);
        }

        private List<ExpenseSummaryModel> Convert_ExpenseListToExpenseSummaryModelList(List<Expense> expenses)
        {
            List<ExpenseSummaryModel> res = new List<ExpenseSummaryModel>();
            res = expenses.Select(e => new ExpenseSummaryModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                CategoryID = e.SubCategory.CategoryID,
                SubCategoryID = e.SubCategoryID,
                Amount = e.Amount,
                CreatedDate = e.CreatedDate,
                CategoryName = e.SubCategory.Category != null ? e.SubCategory.Category.CategoryName : string.Empty,
                SubCategoryName = e.SubCategory != null ? e.SubCategory.SubCategoryName : string.Empty
            }).ToList();
            return res;
        }
    }
}
