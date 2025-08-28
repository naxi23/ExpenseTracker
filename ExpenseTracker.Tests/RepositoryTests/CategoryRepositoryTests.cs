using ExpenseTracker.Application.Common.Repository;
using ExpenseTracker.Domain.DTOs;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Tests.RepositoryTests
{
    [TestCaseOrderer("ExpenseTracker.Tests.PriorityTestOrderer", "ExpenseTracker.Tests")]
    public class CategoryRepositoryTests
    {
        private async Task<ExpenseDBContext> GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ExpenseDBContext>()
                .UseInMemoryDatabase(databaseName: "ExpenseDb_Test")
                .Options;

            var context = new ExpenseDBContext(options);

            // Seed data
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        Id = 1,
                        CategoryName = "Food",
                        SubCategories = new List<SubCategory>
                        {
                            new SubCategory { Id = 1, SubCategoryName = "Groceries" }
                        }
                       
                    },
                    new Category
                    {
                        Id = 2,
                        CategoryName = "Transport",
                        SubCategories = new List<SubCategory>
                        {
                            new SubCategory { Id = 2, SubCategoryName = "Bus" },
                            new SubCategory { Id = 3, SubCategoryName = "Car" }
                        }
                    }
                );
                await context.SaveChangesAsync();
            }

            return context;
        }

        [Fact]
        public async Task GetCategoryList_ReturnsAllCategories()
        {
            // Arrange
            var context = GetDbContextWithData();
            CategoryRepository _categoryRepository = new CategoryRepository(context.Result);
            // Act
            var result = await _categoryRepository.GetCategoryList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.CategoryName == "Food");
            Assert.Contains(result, c => c.CategoryName == "Transport");
        }

        [Fact]
        public async Task GetCategoryById_ReturnsCategory_WhenCategoryExists()
        {
            // Arrange
            var context = GetDbContextWithData();
            CategoryRepository _categoryRepository = new CategoryRepository(context.Result);

            // Act
            var category = await _categoryRepository.GetCategoryById(1);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(1, category.Id);
            Assert.Equal("Food", category.CategoryName);
        }

        [Fact]
        public async Task AddCategory_AddsNewCategoryAndReturnsTrue()
        {
            // Arrange
            var context = GetDbContextWithData();
            CategoryRepository _categoryRepository = new CategoryRepository(context.Result);
            CategoryModel newCategory = new CategoryModel()
            {
                CategoryName = "Shopping"
            };
            // Act
            var result = await _categoryRepository.AddCategory(newCategory);

            // Assert
            Assert.True(result);
            
        }

        [Fact]
        public async Task EditCategory_UpdatesCategoryAndReturnsTrue_WhenCategoryExists()
        {
            // Arrange
            var context = GetDbContextWithData();
            CategoryRepository _categoryRepository = new CategoryRepository(context.Result);
            var existingCategory = new Category { Id = 2, CategoryName = "Transport"};
            var categoryModel = new CategoryModel { Id = 2, CategoryName = "Transportation"};

            // Act
            var result = await _categoryRepository.EditCategory(categoryModel);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCategory_RemovesCategory_WhenCategoryExists()
        {
            // Arrange
            var context = GetDbContextWithData();
            CategoryRepository _categoryRepository = new CategoryRepository(context.Result);
            var prevlist = await _categoryRepository.GetCategoryList();
            // Act
            await _categoryRepository.DeleteCategory(1);
            var currlist = await _categoryRepository.GetCategoryList();
            // Assert
            Assert.Equal(prevlist.Count() - 1, currlist.Count());
        }
    }
}
