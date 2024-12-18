using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class DifficultiesRepository : DifficultiesInterface
    {
        private readonly Context _context;

        public DifficultiesRepository(Context context)
        {
            _context = context;
        }

        public void AddDifficulty(Difficulty difficulty)
        {
            _context.Difficulties.Add(difficulty);
            _context.SaveChanges();
        }

        public void DeleteDifficulty(int id)
        {
            var difficulty = GetDifficultyById(id);
            if (difficulty != null)
            {
                _context.Difficulties.Remove(difficulty);
                _context.SaveChanges();
            }
        }

        public Difficulty GetDifficultyById(int id)
        {
            return _context.Difficulties.FirstOrDefault(d => d.DifficultyId == id);
        }

        public IEnumerable<Difficulty> GetDifficulties()
        {
            return _context.Difficulties.ToList();
        }

        public void UpdateDifficulty(Difficulty newDifficulty)
        {
            if (newDifficulty == null) throw new ArgumentNullException(nameof(newDifficulty));
            var oldDifficulty = GetDifficultyById(newDifficulty.DifficultyId);
            if (oldDifficulty != null)
            {
                oldDifficulty.DifficultyLevel = newDifficulty.DifficultyLevel;
                oldDifficulty.Words = newDifficulty.Words;

                _context.SaveChanges();
            }
        }
    }
}
