using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
