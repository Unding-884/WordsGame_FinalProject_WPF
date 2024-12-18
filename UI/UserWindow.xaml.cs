using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordsGame;
using UI.Properties;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        bool IsThere(char let, string word, int att)
        {
            bool ist = false;
            foreach (char l in word)
            {
                if (l == let)
                    ist = true;
            }
            att += !ist ? 1 : 0;
            AttemptsLabel.Content = attemps < 0 ? "Infinite amount of attempts" : $"Attempts left: {attemps - att - 1}";
            if (att == attemps && attemps > 0)
            {
                PlayGameMenuGroupBox.Visibility = Visibility.Visible;
                GameGroupBox.Visibility = Visibility.Hidden;
                MessageBox.Show("You ran out of attempts",
                        "Oh no!",
                        MessageBoxButton.OK);
            }

            return ist;
        }
        void ButtonColorChange(Button btn, char l)
        {
            if (shown == word) { }
            else
            {
                if (IsThere(l, word, curr_at))
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == l)
                            guess[i] = l;
                        else if (guess[i] == null)
                            guess[i] = ' ';
                    }
                    btn.Background = Brushes.LightGreen;
                }
                else
                {
                    btn.Background = Brushes.DarkRed;
                    curr_at++;
                }
                shown = ShownWord(guess, shown);
                if (shown == word)
                {
                    int w = _wordsRepository.GetWordsWord().FirstOrDefault(w => w.WordText.ToLower() == word.ToLower()).WordId;
                    AddScore(w, true);
                    MessageBox.Show("Congratulations, you guessed the word!",
                            "Congrats!",
                            MessageBoxButton.OK);
                    
                    MainMenuGroupBox.Visibility = Visibility.Visible;
                    GameGroupBox.Visibility = Visibility.Hidden;
                }
            }
        }
        string ShownWord(char[] que, string word)
        {
            word = new string(que);
            WordLabel.Content = word;
            return word;
        }

        private void SaveSettings()
        {
            var backgroundColor = ((SolidColorBrush)this.Background).Color.ToString();
            var textColor = ((SolidColorBrush)this.Foreground).Color.ToString();

            var settings = new List<string>
            {
                backgroundColor,
                textColor
            };

            _txTreadSave.SaveToFile(settings);
        }

        private void LoadSettings()
        {
            var settings = _txTreadSave.ReadFromFile();
            if (settings.Count >= 2)
            {
                var backgroundColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings[0]));
                var textColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(settings[1]));

                this.Background = backgroundColorBrush;
                ChangeBackgroundColor(this, backgroundColorBrush);

                ChangeTextColor(this, textColorBrush);
                this.Foreground = textColorBrush; // Ensure the Foreground property is set
            }
        }

        

        Random rand = new Random();
            string word, shown = "";
            char[] guess = new char[0];
            int attemps, curr_at;
        


        private readonly DifficultiesRepository _difficultiesRepository;
        private readonly WordsRepository _wordsRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly CatWordsRepository _catWordsRepository;
        private readonly ScoreRepository _scoreRepository;

        private MediaPlayer mediaPlayer = new MediaPlayer();
        private List<Uri> playlist = new List<Uri>();
        private int currentSongIndex = 0;

        Context context = new Context();

        private TxTreadSave _txTreadSave;
        public UserWindow()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.txt");

            _txTreadSave = new TxTreadSave(path);

            InitializeComponent();
            
            _difficultiesRepository = new DifficultiesRepository(context);
            _wordsRepository = new WordsRepository(context);
            _categoryRepository = new CategoryRepository(context);
            _catWordsRepository = new CatWordsRepository(context);
            _scoreRepository = new ScoreRepository(context);

            Canvas.SetLeft(MainMenuGroupBox, 0);
            Canvas.SetTop(MainMenuGroupBox, 0);
            Canvas.SetLeft(PlayGameMenuGroupBox, 0);
            Canvas.SetTop(PlayGameMenuGroupBox, 0);

            PassTextBox.Visibility = Visibility.Hidden;
            ProceedButton.Visibility = Visibility.Hidden;
            SettingsGroupBox.Visibility = Visibility.Hidden;

            LoadSettings();


            playlist.Add(new Uri("C:/Users/nazar/source/repos/WordsGame/UI/Resourses/Spectre.mp3"));
            playlist.Add(new Uri("C:/Users/nazar/source/repos/WordsGame/UI/Resourses/Alan-Walker-Fade-_COPYRIGHTED-NCS-Release_.mp3"));
            mediaPlayer.Open(new Uri("C:/Users/nazar/source/repos/WordsGame/UI/Resourses/Spectre.mp3"));
            mediaPlayer.MediaEnded += new EventHandler(Media_Ended);
            




            if (Properties.Settings.Default.IsMusicEnabled)
            {
                mediaPlayer.Play();
            }
        }

        public void NextSong()
        {
            if (playlist.Count > 0)
            {
                currentSongIndex = (currentSongIndex + 1) % playlist.Count; // Loop to start
                mediaPlayer.Open(playlist[currentSongIndex]);
                mediaPlayer.Play();
            }
        }

        private void Media_Ended(object? sender, EventArgs e)
        {
            NextSong();
        }

        private void AddScore(int wordId, bool isCorrect)
        {
            var score = new Score
            {
                date_achieved = DateTime.Now,
                wordID = wordId,
                isCorrect = isCorrect,
                word = context.Words.Find(wordId)
            };
            _scoreRepository.AddScore(score);
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
                CategoryGameComboBox.ItemsSource = _categoryRepository.GetCategories();
                DifficultyGameComboBox.ItemsSource = _difficultiesRepository.GetDifficulties();
                PlayGameMenuGroupBox.Visibility = Visibility.Visible;
                MainMenuGroupBox.Visibility = Visibility.Hidden;
            CategoryGameComboBox.ItemsSource = _categoryRepository.GetCategories();
            DifficultyGameComboBox.ItemsSource = _difficultiesRepository.GetDifficulties();
            PlayGameMenuGroupBox.Visibility = Visibility.Visible;
            MainMenuGroupBox.Visibility = Visibility.Hidden;

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            PassTextBox.Visibility = Visibility.Visible;
            ProceedButton.Visibility = Visibility.Visible;
        }

        private void buttonA_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            char let = char.ToLower(btn.Content.ToString()[0]);
            ButtonColorChange(btn, let);
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            int w = _wordsRepository.GetWordsWord().FirstOrDefault(w => w.WordText.ToLower() == word.ToLower()).WordId;
            AddScore(w, false);
            MainMenuGroupBox.Visibility = Visibility.Visible;
            GameGroupBox.Visibility = Visibility.Hidden;
        }

        private void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            if (PassTextBox.Text == "Admin123!")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else             {
                MessageBox.Show("Incorrect password");
            }
        }

        private void ReturnToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuGroupBox.Visibility = Visibility.Visible;
            SettingsGroupBox.Visibility = Visibility.Hidden;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuGroupBox.Visibility = Visibility.Hidden;
            SettingsGroupBox.Visibility = Visibility.Visible;
        }

        private void ChangeBackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var backgroundColorBrush = btn.Background;
            this.Background = backgroundColorBrush;
            ChangeBackgroundColor(this, backgroundColorBrush);
            SaveSettings();
        }

        private void ChangeBackgroundColor(DependencyObject obj, Brush color)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                if (child is Control control)
                {
                    if (control is Button button && button.Tag?.ToString() == "ColorChangeButton")
                    {
                        continue; // Skip the buttons used to change color
                    }

                    if (control is ComboBox)
                    {
                        continue; // Skip ComboBox controls
                    }

                    control.Background = color;
                    if (control is Button btn)
                    {
                        btn.BorderBrush = Brushes.White;
                        btn.BorderThickness = new Thickness(1);
                    }
                }
                if (child is DependencyObject dependencyObject)
                {
                    ChangeBackgroundColor(dependencyObject, color);
                }
            }
        }

        private void ChangeTextColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var textColorBrush = btn.Background;
            ChangeTextColor(this, textColorBrush);
            this.Foreground = textColorBrush; // Ensure the Foreground property is set
            SaveSettings();

        }

        private void ChangeTextColor(DependencyObject obj, Brush color)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                if (child is Control control)
                {
                    if (control is ComboBox)
                    {
                        continue; // Skip ComboBox controls
                    }
                    control.Foreground = color;
                    if (control is Button button)
                    {
                        button.BorderBrush = Brushes.White;
                        button.BorderThickness = new Thickness(1);
                    }
                }
                if (child is DependencyObject dependencyObject)
                {
                    ChangeTextColor(dependencyObject, color);
                }
            }
        }

        private void ScoreButton_Click(object sender, RoutedEventArgs e)
        {
            var scores = _scoreRepository.GetScores();
            ScoreDataGrid.ItemsSource = scores;
            ScoreGroupBox.Visibility = Visibility.Visible;
        }

        private void ScoreButton_Click_1(object sender, RoutedEventArgs e)
        {
            var scores = _scoreRepository.GetScores();
            ScoreDataGrid.ItemsSource = scores;
            ScoreGroupBox.Visibility = Visibility.Visible;
        }

        private void MainMenuFromScore_Click(object sender, RoutedEventArgs e)
        {
            ScoreGroupBox.Visibility = Visibility.Hidden;
            MainMenuGroupBox.Visibility = Visibility.Visible;

        }

        private void StopMusicButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void RenewMusicButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void _25Button_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Volume = 0.25;
        }

        private void _50Button_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Volume = 0.5;
        }

        private void _75Button_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Volume = 0.75;
        }

        private void _100Button_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Volume = 1;
        }

        private void NextSongButton_Click(object sender, RoutedEventArgs e)
        {
            NextSong();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenuGroupBox.Visibility = Visibility.Visible;
            PlayGameMenuGroupBox.Visibility = Visibility.Hidden;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            attemps = 1;
            curr_at = 0;
            shown = "";
            attemps += (int)AttemptsSlider.Value == 0 ? -6 : (int)AttemptsSlider.Value + 1;

            var selectedCategory = (Category)CategoryGameComboBox.SelectedItem;
            var selectedDifficulty = (Difficulty)DifficultyGameComboBox.SelectedItem;
            try
            {
                var cw = _catWordsRepository.GetCatWords()
                    .Where(cw => cw.CategoryId == selectedCategory.CategoryId).ToList();
                var words = _wordsRepository.GetWordsWord()
                    .Where(w => cw.Any(c => c.WordId == w.WordId) // Compare CategoryId to IDs in cw
                                && w.DifficultyId == selectedDifficulty.DifficultyId).ToList();

                if (words.Any())
                {
                    word = words.ElementAt(rand.Next(0, words.Count)).WordText.ToLower();
                    MessageBox.Show("Done!");
                }
                else
                {
                    MessageBox.Show("No words found for the selected category and difficulty.");
                }

                if(word != null)
                {
                    guess = new char[word.Length];
                    for (int i = 0; i < word.Length; i++)
                    {
                        //guess[i] = '_';
                        shown += "*";
                    }
                    WordLabel.Content = shown;
                    AttemptsLabel.Content = attemps == -5 ? "Infinite amount of attempts" : "Attempts: " + attemps;
                    PlayGameMenuGroupBox.Visibility = Visibility.Hidden;
                    GameGroupBox.Visibility = Visibility.Visible;
                }
                for(int i =0;i<guess.Length;i++)
                {
                    guess[i] = '*';
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Data.ToString());
            }
        }
    }
}
