using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Application.Common.Repository
{
    public class CategoryRepository : ICategoryService
    {
        private readonly ExpenseDBContext _context;
        public CategoryRepository(ExpenseDBContext context) {
            _context = context; 
        }

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            IEnumerable<Category> listOfCategories = await _context.Categories.Include(c => c.SubCategories).ToListAsync();
            return listOfCategories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category? category = await _context.Categories.Include(c => c.SubCategories).Where(x => x.Id == id).FirstOrDefaultAsync();
            return category ?? new Category();
        }

        public async Task<bool> AddCategory(CategoryModel categoryModel)
        {
            try
            {
                Category category = new();
                bool isSuccess = false;
                if (categoryModel != null)
                {
                    category.CategoryName = categoryModel.CategoryName;
                    _context.Add(category);
                    var result = await _context.SaveChangesAsync();
                    isSuccess = result == 1 ? true : false; 
                }
                return isSuccess;
            }
            catch (Exception){
                throw;
            }
        }
        
        public async Task<bool> EditCategory(CategoryModel categoryModel)
        {
            try
            {
                Category? category = await _context.Categories.FindAsync(categoryModel.Id);
                bool isSuccess = false;
                if (category != null)
                {
                    category.CategoryName = categoryModel.CategoryName;
                    _context.Update(category);
                    var result = await _context.SaveChangesAsync();
                    isSuccess = result == 1 ? true : false; 
                }
                return isSuccess;
            }
            catch (Exception){
                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            if (id != 0) {
                Category? category = await _context.Categories.FindAsync(id);
                if (category != null) 
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();  
                }
            }
        }
    }
}
