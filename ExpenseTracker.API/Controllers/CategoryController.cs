using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetCategories")]
        public async Task<ResponseModel<IEnumerable<Category>>> GetCategories()
        {
            ResponseModel<IEnumerable<Category>> responseModel = new();
            try
            {
                responseModel.Data = await _categoryService.GetCategoryList();
                responseModel.Message = "Data Fetched Successfully";
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ResponseModel<Category>> GetCategoryById(int id)
        {
            ResponseModel<Category> responseModel = new();
            try
            {
                responseModel.Data = await _categoryService.GetCategoryById(id);
                responseModel.Message = "Data Fetched Successfully";
                responseModel.Success = true;
            }
            catch (Exception ex) {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }

        [HttpPost("AddCategory")]
        public async Task<ResponseModel> AddCategory([FromBody] CategoryModel model)
        {
            ResponseModel responseModel = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.Message = "Modelstate is not valid!";
                    responseModel.Success = false;
                }
                else {
                    bool isSuccess = await _categoryService.AddCategory(model);
                    if (isSuccess)
                    {
                        responseModel.Message = "Category Added Successfully";
                        responseModel.Success = true;
                    }
                    else {
                        responseModel.Message = "Operation failed while creating Category";
                        responseModel.Success = false;
                    }
                }
            }
            catch (Exception ex) 
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }

        [HttpPut("EditCategory/{id}")]
        public async Task<ResponseModel> EditCategory(int id, [FromBody] CategoryModel model)
        {
            ResponseModel responseModel = new();
            try
            {
                if (id != model.Id)
                {
                    responseModel.Success = false;
                    responseModel.Message = "Id mismatch!";
                }
                else { 
                    bool isSuccess = await _categoryService.EditCategory(model);
                    if (isSuccess)
                    {
                        responseModel.Success = true;
                        responseModel.Message = "Category updated successfully!";
                    }
                    else
                    {
                        responseModel.Success= false;
                        responseModel.Message = "Failed to update category!";
                    }
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ResponseModel> DeleteCategory(int id)
        {
            ResponseModel responseModel = new();
            try
            {
                if (id != 0)
                {
                    await _categoryService.DeleteCategory(id);
                    responseModel.Success = true;
                    responseModel.Message = "Category deleted successfully!";
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "Failed to delete category!";
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }
    }
}
