using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordsGame;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DifficultiesRepository _difficultiesRepository;
        private readonly WordsRepository _wordsRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly CatWordsRepository _catWordsRepository;

        public MainWindow()
        {
            InitializeComponent();
            var context = new Context();
            _difficultiesRepository = new DifficultiesRepository(context);
            _wordsRepository = new WordsRepository(context);
            _categoryRepository = new CategoryRepository(context);
            _catWordsRepository = new CatWordsRepository(context);
            RefreshDifficultiesDataGrid();
            RefreshWordsDataGrid();
            LoadCategories();
            LoadDifficulties();
        }
        private void LoadDifficulties()
        {
            DifficultyComboBox.ItemsSource = _difficultiesRepository.GetDifficulties();
        }
        private void AddDifficulty_Click(object sender, RoutedEventArgs e)
        {
            var difficulty = new Difficulty
            {
                DifficultyLevel = DifficultyLevelTextBox.Text
            };
            _difficultiesRepository.AddDifficulty(difficulty);
            RefreshDifficultiesDataGrid();
        }

        private void UpdateDifficulty_Click(object sender, RoutedEventArgs e)
        {
            var difficulty = _difficultiesRepository.GetDifficulties()
                .FirstOrDefault(d => d.DifficultyLevel == DifficultyLevelTextBox.Text);
            if (difficulty != null)
            {
                difficulty.DifficultyLevel = DifficultyLevelTextBox.Text;
                _difficultiesRepository.UpdateDifficulty(difficulty);
                RefreshDifficultiesDataGrid();
            }
        }

        private void DeleteDifficulty_Click(object sender, RoutedEventArgs e)
        {
            var difficulty = _difficultiesRepository.GetDifficulties()
                .FirstOrDefault(d => d.DifficultyLevel == DifficultyLevelTextBox.Text);
            if (difficulty != null)
            {
                _difficultiesRepository.DeleteDifficulty(difficulty.DifficultyId);
                RefreshDifficultiesDataGrid();
            }
        }

        private void GetDifficulty_Click(object sender, RoutedEventArgs e)
        {
            var difficulty = _difficultiesRepository.GetDifficulties()
                .FirstOrDefault(d => d.DifficultyLevel == DifficultyLevelTextBox.Text);
            if (difficulty != null)
            {
                DifficultyLevelTextBox.Text = difficulty.DifficultyLevel;
            }
        }

        private void RefreshDifficultiesDataGrid()
        {
            DifficultiesDataGrid.ItemsSource = _difficultiesRepository.GetDifficulties().ToList();
        }

        private void AddWord_Click(object sender, RoutedEventArgs e)
        {
            var word = new Word
            {
                WordText = WordTextBox.Text,
                DifficultyId = (int)DifficultyComboBox.SelectedValue
            };

            var selectedCategories = CategoriesListBox.SelectedItems.Cast<Category>().ToList();
            if (selectedCategories.Any())
            {
                _catWordsRepository.AddWordWithCategories(word, selectedCategories.Select(c => c.CategoryId).ToList());
                RefreshWordsDataGrid();
            }
            else
            {
                MessageBox.Show("Please select at least one category.");
            }
        }

        private void UpdateWord_Click(object sender, RoutedEventArgs e)
        {
            var word = _wordsRepository.GetWordsWord()
                .FirstOrDefault(w => w.WordText == WordTextBox.Text);
            if (word != null)
            {
                word.WordText = WordTextBox.Text;
                _wordsRepository.UpdateWord(word);
                RefreshWordsDataGrid();
            }
        }

        private void DeleteWord_Click(object sender, RoutedEventArgs e)
        {
            var word = _wordsRepository.GetWords()
                .FirstOrDefault(w => w.WordText == WordTextBox.Text);
            if (word != null)
            {
                _wordsRepository.DeleteWord(word.WordId);
                RefreshWordsDataGrid();
            }
        }

        private void GetWord_Click(object sender, RoutedEventArgs e)
        {
            var word = _wordsRepository.GetWords()
                .FirstOrDefault(w => w.WordText == WordTextBox.Text);
            if (word != null)
            {
                WordTextBox.Text = word.WordText;
            }
        }

        private void RefreshWordsDataGrid()
        {
            WordsDataGrid.ItemsSource = _wordsRepository.GetWords().ToList();
        }

        private void LoadCategories()
        {
            CategoriesDataGrid.ItemsSource = _categoryRepository.GetCategories();
            CategoriesListBox.ItemsSource = _categoryRepository.GetCategories();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = new Category
            {
                CategoryName = CategoryNameTextBox.Text
            };
            _categoryRepository.AddCategory(category);
            LoadCategories();
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.CategoryName == CategoryNameTextBox.Text);
            if (category != null)
            {
                category.CategoryName = CategoryNameTextBox.Text;
                _categoryRepository.UpdateCategory(category);
                LoadCategories();
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.CategoryName == CategoryNameTextBox.Text);
            if (category != null)
            {
                _categoryRepository.DeleteCategory(category.CategoryId);
                LoadCategories();
            }
        }

        private void GetCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.CategoryName == CategoryNameTextBox.Text);
            if (category != null)
            {
                CategoryNameTextBox.Text = category.CategoryName;
            }
        }

        private void ReturnToGame_Click(object sender, RoutedEventArgs e)
        {
            UserWindow mainWindow = new UserWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}