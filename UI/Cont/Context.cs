using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using WordsGame;

namespace UI.Cont
{
    public class Context : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CatWord> CatWords { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Word to Difficulty Relationship
            modelBuilder.Entity<Word>()
                .HasOne(w => w.Difficulty)
                .WithMany(d => d.Words)
                .HasForeignKey(w => w.DifficultyId);

            // Many-to-many Relationship between Word and Category
            modelBuilder.Entity<CatWord>()
                .HasKey(cw => new { cw.WordId, cw.CategoryId });

            modelBuilder.Entity<CatWord>()
                .HasOne(cw => cw.Word)
                .WithMany(w => w.CatWords)
                .HasForeignKey(cw => cw.WordId);

            modelBuilder.Entity<CatWord>()
                .HasOne(cw => cw.Category)
                .WithMany(c => c.CatWords)
                .HasForeignKey(cw => cw.CategoryId);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.word)
                .WithMany(w => w.Scores)
                .HasForeignKey(s => s.wordID);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost,1433;Database=words_db;User=SA;Password=Admin123!;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
