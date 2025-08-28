using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.MVC.Controllers
{
    public class DashboardController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5198/api/Dashboard/");
        private readonly HttpClient _httpClient;
        public DashboardController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "GetExpenseReportSummary");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var model = JsonSerializer.Deserialize<ExpenseReportSummaryViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (model == null)
                    {
                        return View("Error", new ErrorViewModel { Message = "Failed to load expense report summary." });
                    }
                    return View(model);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return View("NotFound");
                }
                else
                {
                    return View("Error", new ErrorViewModel { Message = "API returned error: " + response.ReasonPhrase });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = "Exception: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyExpenseDetails(int month = 0, int year = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "GetMonthlyExpenseDetails?month="+month+"&year="+year);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var model = JsonSerializer.Deserialize<ExpenseReportSummaryViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (model == null)
                    {
                        return View("Error", new ErrorViewModel { Message = "Failed to load expense report summary." });
                    }
                    return PartialView("_MonthlyExpenseSummaryPartial", model);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return View("NotFound");
                }
                else
                {
                    return View("Error", new ErrorViewModel { Message = "API returned error: " + response.ReasonPhrase });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = "Exception: " + ex.Message });
            }
            
        }
        
        [HttpGet]
        public async Task<IActionResult> GetYearlyExpenseDetails(int year = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "GetYearlyExpenseDetails?year="+year);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var model = JsonSerializer.Deserialize<ExpenseReportSummaryViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (model == null)
                    {
                        return View("Error", new ErrorViewModel { Message = "Failed to load expense report summary." });
                    }
                    return PartialView("_YearlyExpenseSummaryPartial", model);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return View("NotFound");
                }
                else
                {
                    return View("Error", new ErrorViewModel { Message = "API returned error: " + response.ReasonPhrase });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = "Exception: " + ex.Message });
            }
            
        }

    }
}
