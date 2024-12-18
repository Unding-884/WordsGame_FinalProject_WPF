using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class CategoryRepository : ICategoryInterface
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category newCategory)
        {
            if (newCategory == null) throw new ArgumentNullException(nameof(newCategory));
            var oldCategory = GetCategoryById(newCategory.CategoryId);
            if (oldCategory != null)
            {
                oldCategory.CategoryName = newCategory.CategoryName;
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            _context.Categories.Remove(GetCategoryById(id));
            _context.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
}