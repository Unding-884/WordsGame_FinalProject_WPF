namespace WordsGame
{
    public class Difficulty
    {
        public int DifficultyId { get; set; } // Primary Key
        public string DifficultyLevel { get; set; }
        public ICollection<Word> Words { get; set; } // Navigation Property
        
        public override string ToString()
        {
            return DifficultyLevel;
        }
    }
}
