using ExpenseTracker.Domain.DTOs;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface IDashboardService
    {
        Task<List<ExpenseMonthlySummaryViewModel>> GetMonthlyExpenseList(int month = 0, int year = 0);
        Task<List<ExpenseYearlySummaryViewModel>> GetYearlyExpenseList(int year = 0);
    }
}
