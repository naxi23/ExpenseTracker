using System.Runtime.InteropServices;
using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("GetExpenseReportSummary")]
        public async Task<IActionResult> GetExpenseReportSummary()
        {
            try
            {
                var model = new ExpenseReportSummaryViewModel
                {
                    CurrentMonth = DateTime.Now.Month,
                    CurrentYear = DateTime.Now.Year,
                    ExpenseMonthlySummary = await _dashboardService.GetMonthlyExpenseList(),
                    ExpenseYearlySummary = await _dashboardService.GetYearlyExpenseList()
                };
                if (model == null)
                {
                    return NotFound(new { Message = "No records not found." });
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
        
        [HttpGet("GetMonthlyExpenseDetails")]
        public async Task<IActionResult> GetMonthlyExpenseDetails(int month = 0, int year = 0)
        {
            try
            {
                var model = new ExpenseReportSummaryViewModel
                {
                    CurrentMonth = month == 0 ? DateTime.Now.Month : month,
                    CurrentYear = year == 0 ? DateTime.Now.Month : year,
                    ExpenseMonthlySummary = await _dashboardService.GetMonthlyExpenseList(month, year),
                    ExpenseYearlySummary = await _dashboardService.GetYearlyExpenseList(year)
                };
                if (model == null)
                {
                    return NotFound(new { Message = "No records not found." });
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
        
        [HttpGet("GetYearlyExpenseDetails")]
        public async Task<IActionResult> GetYearlyExpenseDetails(int year = 0)
        {
            try
            {
                var model = new ExpenseReportSummaryViewModel
                {
                    CurrentYear = year == 0 ? DateTime.Now.Year : year,
                    ExpenseYearlySummary = await _dashboardService.GetYearlyExpenseList(year)
                };
                if (model == null)
                {
                    return NotFound(new { Message = "No records not found." });
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
