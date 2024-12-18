using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsGame;

namespace WordsGame
{
    public interface DifficultiesInterface
    {
        void AddDifficulty(Difficulty difficulty);
        void UpdateDifficulty(Difficulty difficulty);
        void DeleteDifficulty(int id);
        Difficulty GetDifficultyById(int id);
        IEnumerable<Difficulty> GetDifficulties();
    }
}
