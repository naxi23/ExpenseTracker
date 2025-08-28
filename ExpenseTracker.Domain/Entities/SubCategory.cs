using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        public string SubCategoryName { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public ICollection<Expense> Expenses { get; set; }

    }
}
