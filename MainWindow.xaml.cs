using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chapter_1_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            SetUpGame();

        }


        TextBlock lastTextBlockClicked;
        bool findingMatch = false;


        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBox.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                btn_Exit.Visibility = Visibility.Visible;
                btn_PlayAgain.Visibility = Visibility.Visible;
                finalTimeTextBox.Visibility = Visibility.Visible;
                finalTimeTextBox.Text = "Your time was " + timeTextBox.Text;
                timeTextBox.Visibility = Visibility.Hidden;
            }
        }


        private void SetUpGame()
        {
            btn_Exit.Visibility = Visibility.Hidden;
            btn_PlayAgain.Visibility = Visibility.Hidden;
            finalTimeTextBox.Visibility = Visibility.Hidden;
            timeTextBox.Visibility = Visibility.Visible;

            List<string> animalEmoji = new List<string>()
            {
                "🐔", "🐔",
                "🐍", "🐍",
                "🐷", "🐷",
                "🐭", "🐭",
                "🐸", "🐸",
                "🐫", "🐫",
                "🦘", "🦘",
                "🦢", "🦢"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBox")
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    textBlock.Visibility = Visibility.Visible;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else
            {
                if (textBlock.Text == lastTextBlockClicked.Text)
                {
                    matchesFound++;
                    textBlock.Visibility = Visibility.Hidden;
                    findingMatch = false;
                }
                else
                {
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                    findingMatch = false;
                }
            }
        }
        

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        private void Btn_PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            SetUpGame();
        }
    }
}
