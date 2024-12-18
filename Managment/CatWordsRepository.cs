using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsGame;

namespace WordsGame
{
    public class CatWordsRepository : ICatWords
    {
        private readonly Context _context;

        public CatWordsRepository(Context context)
        {
            _context = context;
        }
        public CatWord GetCatWordById(int id)
        {
            return _context.CatWords.FirstOrDefault(cw => cw.CatWordId == id);
        }

        public IEnumerable<CatWord> GetCatWords()
        {
            return _context.CatWords.ToList();
        }
        public void AddWordWithCategories(Word word, List<int> categoryIds)
        {
            _context.Words.Add(word);
            _context.SaveChanges();

            foreach (var categoryId in categoryIds)
            {
                _context.CatWords.Add(new CatWord { WordId = word.WordId, CategoryId = categoryId });
            }
            _context.SaveChanges();
        }
    }
}
