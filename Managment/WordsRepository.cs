using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class WordsRepository : WordsInterface
    {
        private readonly Context _context;
        public WordsRepository(Context context)
        {
            _context = context;
        }

        public void AddWord(Word word, int categoryId)
        {
            _context.Words.Add(word);
            _context.SaveChanges();

            //var catWord = new CatWord
            //{
            //    WordId = word.WordId,
            //    CategoryId = categoryId
            //};
            //_context.CatWords.Add(catWord);
            //_context.SaveChanges();
        }

        public void DeleteWord(int id)
        {
            _context.Words.Remove(GetWordById(id));
            _context.SaveChanges();
        }

        public Word GetWordById(int id)
        {
            return _context.Words.FirstOrDefault(w => w.WordId == id);
        }

        public IEnumerable<WordViewModel> GetWords()
        {
            return _context.Words
                .Include(w => w.CatWords) // Include Difficulty
                .Select(w => new WordViewModel
                {
                    WordId = w.WordId,
                    WordText = w.WordText,
                    DifficultyLevel = w.Difficulty.DifficultyLevel // Include DifficultyLevel
                })
                .ToList();
        }

        public IEnumerable<Word> GetWordsWord()
        {
            return _context.Words
                .Select(w => new Word
                {
                    WordId = w.WordId,
                    WordText = w.WordText,
                    DifficultyId = w.Difficulty.DifficultyId // Include DifficultyLevel
                })
                .ToList();
        }

        public void UpdateWord(Word newWord)
        {
            if (newWord == null) throw new ArgumentNullException(nameof(newWord));
            var oldWord = GetWordById(newWord.WordId);
            if (oldWord != null)
            {
                oldWord.WordText = newWord.WordText;
                oldWord.DifficultyId = newWord.DifficultyId;

                _context.SaveChanges();
            }
        }
    }
  }

