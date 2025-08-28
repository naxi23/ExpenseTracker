using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Application.Common.Repository
{
    public class DashboardRepository: IDashboardService
    {
        private readonly ExpenseDBContext _context;
        public DashboardRepository(ExpenseDBContext context) 
        {
            _context = context;
        }

        public async Task<List<ExpenseMonthlySummaryViewModel>> GetMonthlyExpenseList(int month = 0, int year = 0)
        {
            List<ExpenseMonthlySummaryViewModel> list = await _context.Expenses
                               .Include(e => e.SubCategory)
                               .Where(e => e.CreatedDate.Month == (month > 0 ? month : DateTime.Now.Month) && e.CreatedDate.Year == (year > 0 ? year : DateTime.Now.Year))
                               .GroupBy(x => new { x.SubCategoryID, x.SubCategory.SubCategoryName })
                               .Select(g => new ExpenseMonthlySummaryViewModel
                               {
                                   Category = g.Key.SubCategoryName,
                                   SpendAmount = g.Sum(e => e.Amount)
                               }).ToListAsync();
            return list;
        }

        public async Task<List<ExpenseYearlySummaryViewModel>> GetYearlyExpenseList(int year = 0)
        {
            List<ExpenseYearlySummaryViewModel> list = await _context.Expenses
                               .Include(e => e.SubCategory)
                               .Where(e => e.CreatedDate.Year == (year > 0 ? year : DateTime.Now.Year))
                               .GroupBy(x => new { x.SubCategoryID, x.SubCategory.SubCategoryName })
                               .Select(g => new ExpenseYearlySummaryViewModel
                               {
                                   Category = g.Key.SubCategoryName,
                                   SpendAmount = g.Sum(e => e.Amount)
                               }).ToListAsync();
            return list;
        }
    }
}
