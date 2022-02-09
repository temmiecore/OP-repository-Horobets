using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.IO;

namespace LabOne
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>


    public partial class Window3 : Window
    {
        static Random rnd = new Random();
        static int player = 1, winO = 0, winX = 0;

        public Button[,] ButtonArray = new Button[5, 5];

        public Window3()
        {
            InitializeComponent();
            foreach (Button b in GameTable.Children.OfType<Button>())
            { ButtonArray[int.Parse(b.Tag.ToString().Substring(0, 1)), int.Parse(b.Tag.ToString().Substring(1, 1))] = b; }
            Stats.Content = "Wins:\nO - " + winO + "\nX - " + winX;
            if (File.Exists("StatsTTT.txt") == false)
                File.WriteAllText("StatsTTT.txt", Stats.Content.ToString());
        }

        private void _3ToMain_Click(object sender, RoutedEventArgs e)
        {
            string[] oldtxt = File.ReadAllLines("StatsTTT.txt"); int i = 0, line = 1;
            while (line < 3)
            {
                char buffer = ' ';
                if (int.TryParse(oldtxt[line].Substring(i, 1), out int num) == true)
                {
                    if (line == 1)
                    { buffer = (char)(num + 48); num += winO; i = 0; }
                    else
                    { buffer = (char)(num + 48); num += winX; }
                    oldtxt[line] = oldtxt[line].Replace(buffer, (char)(num + 48));
                    line++;
                }
                
                i++; 
            }
            File.WriteAllLines("StatsTTT.txt", oldtxt);

            ResetTable();
            winX = 0; winO = 0;
            MainWindow nwc = new MainWindow();
            Hide();
            nwc.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Content == null)
            {
                if (player % 2 == 0)
                {
                    btn.Content = "O";
                    CheckWinLose("O", int.Parse(btn.Tag.ToString().Substring(0, 1)), int.Parse(btn.Tag.ToString().Substring(1, 1)));
                    player++;
                }
                else if (player % 2 == 1)
                {
                    btn.Content = "X";
                    CheckWinLose("X", int.Parse(btn.Tag.ToString().Substring(0, 1)), int.Parse(btn.Tag.ToString().Substring(1, 1)));
                    player++;
                }
            }

        }


        //AI


        private void CheckWinLose(string sign, int X, int Y)
        {
            int Hor = 0, Ver = 0, DigMain = 0, DigReverse = 0, tie = 1;
            for (int i = 0; i < 5; i++)
            {
                if (ButtonArray[i, Y].Content == sign)
                    Ver++;
                if (ButtonArray[X, i].Content == sign)
                    Hor++;
                for (int j = 0; j < 5; j++)
                {
                    if ((i + j) == (X + Y) && ButtonArray[i, j].Content == sign)
                    { DigReverse++; }
                    if ((i - j) == (X - Y) && ButtonArray[i, j].Content == sign)
                    { DigMain++; }
                }
            }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (ButtonArray[i, j].Content == null)
                        tie *= 0;
                }

            if (tie == 1)
            {
                if (MessageBox.Show("Tie!" + "\nOK - retry, Cancel - Main menu.", "Tie", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    ResetTable();
                else
                {
                    MainWindow nwc = new MainWindow();
                    Hide();
                    nwc.Show();
                }

            }

            else if (Ver == 5 || Hor == 5 || DigMain == 5 || DigReverse == 5)
            {
                if (sign == "O")
                    winO++;
                else
                    winX++;

                if (MessageBox.Show("Player " + sign + " Wins!\nOK - retry, Cancel - Main menu.", "Win", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    ResetTable();
                else
                {
                    MainWindow nwc = new MainWindow();
                    Hide();
                    nwc.Show();
                }
                ResetTable();
            }

        }

        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(File.ReadAllText("StatsTTT.txt"), "Stats", MessageBoxButton.OK);
        }

        private void ResetTable()
        {
            Stats.Content = "Wins:\nO - " + winO + "\nX - " + winX;
            foreach (Button btn in GameTable.Children.OfType<Button>())
            {
                btn.Content = null;
                btn.IsEnabled = true;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game reset!", "Reset", MessageBoxButton.OK);
            winX = 0; winO = 0;
            ResetTable();
        }
    }
}
