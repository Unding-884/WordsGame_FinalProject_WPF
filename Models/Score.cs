using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class Score
    {
        public int id { get; set; }
        public DateTime date_achieved { get; set; }
        public int wordID { get; set; }
        public bool isCorrect { get; set; }
        public Word word { get; set; }
    }
}
