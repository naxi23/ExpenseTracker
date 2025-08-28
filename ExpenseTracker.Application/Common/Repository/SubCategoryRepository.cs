using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Application.Common.Repository
{
    public class SubCategoryRepository : ISubCategoryService
    {
        private readonly ExpenseDBContext _context;
        public SubCategoryRepository(ExpenseDBContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesList()
        {
            IEnumerable<SubCategory> listOfSubCategories = await _context.SubCategories.ToListAsync();
            return listOfSubCategories;
        }
        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            SubCategory? subcategory = await _context.SubCategories.Where(x => x.Id == id).FirstOrDefaultAsync();
            return subcategory ?? new SubCategory();
        }
        public async Task<bool> AddSubCategory(SubCategoryModel subcategoryModel)
        {
            try
            {
                SubCategory subcategory = new();
                bool isSuccess = false;
                if (subcategoryModel != null)
                {
                    subcategory.SubCategoryName = subcategoryModel.SubCategoryName;
                    subcategory.CategoryID = subcategoryModel.CategoryID;
                    _context.Add(subcategory);
                    var result = await _context.SaveChangesAsync();
                    isSuccess = result == 1 ? true : false;
                }
                return isSuccess;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EditSubCategory(SubCategoryModel subcategoryModel)
        {
            try
            {
                SubCategory? subcategory = await _context.SubCategories.FindAsync(subcategoryModel.Id);
                bool isSuccess = false;
                if (subcategory != null)
                {
                    subcategory.SubCategoryName = subcategoryModel.SubCategoryName;
                    subcategory.CategoryID = subcategoryModel.CategoryID;
                    _context.Update(subcategory);
                    var result = await _context.SaveChangesAsync();
                    isSuccess = result == 1 ? true : false;
                }
                return isSuccess;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteSubCategory(int id)
        {
            if (id != 0)
            {
                SubCategory? subcategory = await _context.SubCategories.FindAsync(id);
                if (subcategory != null)
                {
                    _context.SubCategories.Remove(subcategory);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
