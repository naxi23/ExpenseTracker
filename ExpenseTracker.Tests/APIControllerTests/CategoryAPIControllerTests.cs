using ExpenseTracker.API.Controllers;
using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;
using Moq;

namespace ExpenseTracker.Tests.APIControllerTests
{
    public class CategoryAPIControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoryController _controller;

        public CategoryAPIControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetCategories_ReturnsSuccessResponse_WithData()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "Investment" },
                new Category { Id = 2, CategoryName = "Expense" }
            };
            _mockCategoryService.Setup(s => s.GetCategoryList())
                .ReturnsAsync(categories);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Data Fetched Successfully", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(2, ((List<Category>)result.Data).Count);
        }

        [Fact]
        public async Task GetCategories_ReturnsFailureResponse_OnException()
        {
            // Arrange
            _mockCategoryService.Setup(s => s.GetCategoryList())
                .ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.GetCategories();

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Some error", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsSuccessResponse_WithCategory()
        {
            // Arrange
            var category = new Category { Id = 1, CategoryName = "Investment" };
            _mockCategoryService.Setup(s => s.GetCategoryById(1))
                .ReturnsAsync(category);

            // Act
            var result = await _controller.GetCategoryById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Data Fetched Successfully", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsFailureResponse_OnException()
        {
            // Arrange
            _mockCategoryService.Setup(s => s.GetCategoryById(1))
                .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.GetCategoryById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task AddCategory_ReturnsSuccessResponse_WhenModelValidAndServiceSucceeds()
        {
            // Arrange
            var model = new CategoryModel { Id = 1, CategoryName = "Investment_Test" };
            _mockCategoryService.Setup(s => s.AddCategory(model)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddCategory(model);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Category Added Successfully", result.Message);
        }

        [Fact]
        public async Task AddCategory_ReturnsFailureResponse_WhenModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("CategoryName", "Required");
            var model = new CategoryModel();

            // Act
            var result = await _controller.AddCategory(model);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Modelstate is not valid!", result.Message);
        }

        [Fact]
        public async Task AddCategory_ReturnsFailureResponse_WhenServiceFails()
        {
            // Arrange
            var model = new CategoryModel { Id = 1, CategoryName = "Cat1" };
            _mockCategoryService.Setup(s => s.AddCategory(model)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddCategory(model);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Operation failed while creating Category", result.Message);
        }

        [Fact]
        public async Task EditCategory_ReturnsSuccessResponse_WhenIdMatchesAndServiceSucceeds()
        {
            // Arrange
            var model = new CategoryModel { Id = 1, CategoryName = "Investment" };
            _mockCategoryService.Setup(s => s.EditCategory(model)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditCategory(1, model);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Category updated successfully!", result.Message);
        }

        [Fact]
        public async Task EditCategory_ReturnsFailureResponse_WhenIdMismatch()
        {
            // Arrange
            var model = new CategoryModel { Id = 2, CategoryName = "Expense" };

            // Act
            var result = await _controller.EditCategory(1, model);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Id mismatch!", result.Message);
        }

        [Fact]
        public async Task EditCategory_ReturnsFailureResponse_WhenServiceFails()
        {
            // Arrange
            var model = new CategoryModel { Id = 1, CategoryName = "Investment" };
            _mockCategoryService.Setup(s => s.EditCategory(model)).ReturnsAsync(false);

            // Act
            var result = await _controller.EditCategory(1, model);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Failed to update category!", result.Message);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsSuccessResponse_WhenIdIsValid()
        {
            // Arrange
            _mockCategoryService.Setup(s => s.DeleteCategory(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCategory(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Category deleted successfully!", result.Message);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsFailureResponse_WhenIdIsZero()
        {
            // Act
            var result = await _controller.DeleteCategory(0);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Failed to delete category!", result.Message);
        }
    }
}
