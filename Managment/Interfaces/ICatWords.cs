using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public interface ICatWords
    {
        CatWord GetCatWordById(int id);
        IEnumerable<CatWord> GetCatWords();
    }
}
