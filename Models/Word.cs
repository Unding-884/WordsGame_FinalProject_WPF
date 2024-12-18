using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsGame;

namespace WordsGame
{
    public class Word
    {
        public int WordId { get; set; } // Primary Key
        public string WordText { get; set; }
        public int DifficultyId { get; set; } // Foreign Key
        public Difficulty Difficulty { get; set; } // Navigation Property
        public ICollection<CatWord> CatWords { get; set; } = new List<CatWord>(); // Navigation Property
        public ICollection<Score> Scores { get; set; }

        public override string ToString()
        {
            return WordText;
        }
    }
}
