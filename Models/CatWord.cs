using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsGame;

namespace WordsGame
{
    public class CatWord
    {
        public int CatWordId { get; set; } // Primary Key
        public int WordId { get; set; } // Foreign Key
        public int CategoryId { get; set; } // Foreign Key
        public Word Word { get; set; } // Navigation Property
        public Category Category { get; set; } // Navigation Property
    }
}
