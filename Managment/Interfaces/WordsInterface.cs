using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public interface WordsInterface// : IDisposable
    {
        void AddWord(Word word, int categoryId);
        void UpdateWord(Word word);
        void DeleteWord(int id);
        Word GetWordById(int id);
        IEnumerable<WordViewModel> GetWords();
    }
}
