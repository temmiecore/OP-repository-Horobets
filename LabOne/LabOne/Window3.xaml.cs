using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

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


        }

        private void _3ToMain_Click(object sender, RoutedEventArgs e)
        {
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



        /*ComputerTurn(int.Parse(btn.Tag.ToString().Substring(0, 1)), int.Parse(btn.Tag.ToString().Substring(1, 1)));*/

        /* private void ComputerTurn(int PlayerX, int PlayerY)
         {
             Dictionary<string, int> Counters = new Dictionary<string, int>() { { "Row", 1 }, { "Col", 1 }, { "DMain", 1 }, { "DReverse", 1 } };
             int Max = int.MinValue; string counter = "";
             for (int i = 0; i < 5; i++)
                 for (int j = 0; j < 5; j++)
                 {
                     if ((i + j) == (PlayerX + PlayerY) && ButtonArray[i, j].Content == "O")
                     { Counters["DReverse"]++; continue; }
                     if ((i - j) == (PlayerX - PlayerY) && ButtonArray[i, j].Content == "O")
                     { Counters["DMain"]++; continue; }
                     if (i == PlayerX && ButtonArray[i, j].Content == "O")
                     { Counters["Row"]++; continue; }
                     if (j == PlayerY && ButtonArray[i, j].Content == "O")
                     { Counters["Col"]++; continue; }
                 }
             foreach (KeyValuePair<string, int> a in Counters)
             {
                 if (a.Value > Max)
                     counter = a.Key;
                 else if (a.Value == Max && rnd.NextDouble() > 0.5)
                     counter = a.Key;
             }
             CompChoise(PlayerX, PlayerY, counter);
             Counters.Clear();
         }           

         private void CompChoise(int PlayerX, int PlayerY, string choise)
         {

             switch (choise)
             {
                 case "Row":
                     {
                         while (true)
                         {
                             int j = 0;
                             if (ButtonArray[PlayerX, j % 5].Content == null && rnd.NextDouble() > 0.6)
                             { ButtonArray[PlayerX, j % 5].Content = "X"; break; }
                             j++;
                         }
                         break;
                     }
                 case "Col":
                     {
                         while (true)
                         {
                             int i = 0;
                             if (ButtonArray[i % 5, PlayerY].Content == null && rnd.NextDouble() > 0.6)
                             { ButtonArray[i % 5, PlayerY].Content = "X"; break; }
                             i++;
                         }
                         break;
                     }
                 case "DReverse":
                     {
                         for (int i = 0; i < 25; i++)
                         {
                             for (int j = 0; j < 25; j++)
                             {
                                 if (((i%5) + (j%5)) == (PlayerX + PlayerY))
                                     if (ButtonArray[i%5, j%5].Content == null && rnd.NextDouble() > 0.6)
                                     { ButtonArray[i%5, j%5].Content = "X"; break; }

                             }
                             break;
                         }
                         break;
                     }
                 case "DMain":
                     {
                         for (int i = 0; i < 25; i++)
                         {
                             for (int j = 0; j < 25; j++)
                             {
                                 if (Math.Abs((i%5) - (j%5)) == Math.Abs(PlayerX - PlayerY))
                                     if (ButtonArray[i%5, j%5].Content == null && rnd.NextDouble() > 0.6)
                                     { ButtonArray[i%5, j%5].Content = "X"; break; }
                             }
                             break;
                         }
                         break;
                     }
             }

         }*/

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

            if (Ver == 5 || Hor == 5 || DigMain == 5 || DigReverse == 5)
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
                Stats.Content = "Wins:\nO - " + winO + "\nX - " + winX;
                ResetTable();
            }

        }

        private void ResetTable()
        {
            foreach (Button btn in GameTable.Children.OfType<Button>())
            {
                btn.Content = null;
                btn.IsEnabled = true;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game reset!", "Reset", MessageBoxButton.OK);
            ResetTable();
        }
    }
}
