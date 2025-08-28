using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Application.Common.Interface;
using System.Text.Json;
using System.Text;

namespace ExpenseTracker.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5198/api/Category/");
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}GetCategories");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ResponseModel<IEnumerable<Category>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(result?.Data);
            }
            return View("Error");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {

            var json = JsonSerializer.Serialize(categoryModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}AddCategory", content);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (result != null && result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoryModel);
        }

        public async Task<IActionResult> Edit(int id = 0)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}GetCategoryById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ResponseModel<Category>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result != null && result.Success)
                    return View(result.Data);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel categoryModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(categoryModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}EditCategory/{categoryModel.Id}", content);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (result != null && result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoryModel);
        }

        public async Task<IActionResult> Delete(int id = 0)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}GetCategoryById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ResponseModel<Category>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result != null && result.Success)
                    return View(result.Data);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id = 0)
        {
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}DeleteCategory/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (result != null && result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Error");
        }
    }
}
