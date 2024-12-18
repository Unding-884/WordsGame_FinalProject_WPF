﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public interface ICategoryInterface
    {
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        Category GetCategoryById(int id);
        IEnumerable<Category> GetCategories();
    }
}