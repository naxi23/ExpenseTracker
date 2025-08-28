using ExpenseTracker.Domain.DTOs;

namespace ExpenseTracker.Domain.DTOs
{
    public class ExpenseReportSummaryViewModel
    {
        public int CurrentMonth {  get; set; }
        public int CurrentYear { get; set; }
        public List<ExpenseMonthlySummaryViewModel> ExpenseMonthlySummary{ get; set; }
        public List<ExpenseYearlySummaryViewModel> ExpenseYearlySummary { get; set; }
    }
}
