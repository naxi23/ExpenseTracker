using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryList();
        Task<Category> GetCategoryById(int id);
        Task<bool> AddCategory(CategoryModel category);
        Task<bool> EditCategory(CategoryModel category);
        Task DeleteCategory(int id);
    }
}
