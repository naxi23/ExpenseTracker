using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.DTOs
{
    public class CategoryModel
    {
        public int Id { get; set; } 
        public string CategoryName { get; set; } 
    }
}
