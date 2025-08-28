using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Common.Interface
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategory>> GetSubCategoriesList();
        Task<SubCategory> GetSubCategoryById(int id);
        Task<bool> AddSubCategory(SubCategoryModel subcategory);
        Task<bool> EditSubCategory(SubCategoryModel subcategory);
        Task DeleteSubCategory(int id);
    }
}
