using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.DTOs
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryID { get; set; }
    }
}
