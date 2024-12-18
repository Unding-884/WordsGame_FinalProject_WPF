using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class ScoreRepository : IScore
    {
        private readonly Context _context;

        public ScoreRepository(Context context)
        {
            _context = context;
        }

        public void AddScore(Score score)
        {
            _context.Scores.Add(score);
            _context.SaveChanges();
        }

        public Score GetScoreById(int id)
        {
            return _context.Scores.FirstOrDefault(s => s.id == id);
        }

        public IEnumerable<Score> GetScores()
        {
            return _context.Scores.ToList();
        }
    }
}
