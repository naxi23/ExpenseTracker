using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Application.Common.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.MVC.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly ExpenseDBContext _context;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;
        public SubCategoriesController(ExpenseDBContext context, ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _context = context;
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<SubCategory> model = await _subCategoryService.GetSubCategoriesList();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryID"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryModel subCategoryModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _subCategoryService.AddSubCategory(subCategoryModel);
                return result ? RedirectToAction(nameof(Index)) : View(new SubCategory());
            }
            ViewData["CategoryID"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "CategoryName");
            return View(new SubCategory());
        }

        public async Task<IActionResult> Edit(int id = 0)
        {
            var subcategory = await _subCategoryService.GetSubCategoryById(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "CategoryName");
            return View(subcategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubCategoryModel subCategory)
        {
            if (subCategory != null)
            {
                bool isSucess = await _subCategoryService.EditSubCategory(subCategory);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["CategoryID"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "CategoryName");
                return View(new SubCategory());
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subCategory = await _subCategoryService.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "CategoryName");
            return View(subCategory);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
