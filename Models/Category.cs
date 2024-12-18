using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class Category
    {
        public int CategoryId { get; set; } // Primary Key
        public string CategoryName { get; set; }
        public ICollection<CatWord> CatWords { get; set; } // Navigation Property

        public override string ToString()
        {
            return CategoryName;
        }
    }
}
