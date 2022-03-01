using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Linq;


namespace LabTwo._2
{
    class Window3
    {
        Button[,] ButtonArr;
        Window wn = new Window();
        static Random rnd = new Random();
        static int player = 1, winO = 0, winX = 0;
        Label Stats = new Label();

        public Window3()
        {
            initControls();
        }

        private void initControls()
        {
            wn.Title = "Tic-Tac-Toe"; wn.Height = 580; wn.Width = 530; wn.Background = Brushes.Lavender;
            Grid grid = new Grid();
            grid.Width = 530; grid.Height = 580;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            RowDefinition[] rows = new RowDefinition[10];
            ColumnDefinition[] cols = new ColumnDefinition[7];
            for (int i = 0; i < 10; i++)
            {
                rows[i] = new RowDefinition();
                grid.RowDefinitions.Add(rows[i]);
            }
            for (int i = 0; i < 7; i++)
            {
                cols[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(cols[i]);
            }

            //Buttons
            ButtonArr = new Button[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    ButtonArr[i, j] = new Button();
                    ButtonArr[i, j].FontSize = 42; ButtonArr[i, j].FontFamily = new FontFamily("Comic Sans MS"); ButtonArr[i, j].Tag = i + "" + j;
                    ButtonArr[i, j].Click += Button_Click;
                    Grid.SetRow(ButtonArr[i, j], i + 1);
                    Grid.SetColumn(ButtonArr[i, j], j + 1);
                    grid.Children.Add(ButtonArr[i, j]);
                }

            Button tomain = new Button(); tomain.FontSize = 24; tomain.FontFamily = new FontFamily("SimSun-ExtB"); tomain.Content = "To main";
            tomain.Click += _3ToMain_Click;
            Grid.SetRow(tomain, 8); Grid.SetColumn(tomain, 4); Grid.SetColumnSpan(tomain, 2);
            Button reset = new Button(); reset.FontSize = 24; reset.FontFamily = new FontFamily("SimSun-ExtB"); reset.Content = "Reset";
            reset.Click += Reset_Click;
            Grid.SetRow(reset, 8); Grid.SetColumn(reset, 1); Grid.SetColumnSpan(reset, 2);
            grid.Children.Add(tomain); grid.Children.Add(reset);

            Stats.Content = "Wins:\nO - " + winO + "\nX - " + winX; ; Stats.FontSize = 32; Stats.FontFamily = new FontFamily("SimSun-ExtB");
            Stats.HorizontalContentAlignment = HorizontalAlignment.Center;
            Grid.SetRow(Stats, 6); Grid.SetColumn(Stats, 2); Grid.SetRowSpan(Stats, 2); Grid.SetColumnSpan(Stats, 3);
            grid.Children.Add(Stats);


            wn.Content = grid;
            wn.Show();
        }


        private void _3ToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nwc = new MainWindow();
            wn.Hide();
            nwc.Show();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game reset!", "Reset", MessageBoxButton.OK);
            winX = 0; winO = 0;
            ResetTable();
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


        private void CheckWinLose(string sign, int X, int Y)
        {
            int Hor = 0, Ver = 0, DigMain = 0, DigReverse = 0, tie = 1;
            for (int i = 0; i < 5; i++)
            {
                if (ButtonArr[i, Y].Content == sign)
                    Ver++;
                if (ButtonArr[X, i].Content == sign)
                    Hor++;
                for (int j = 0; j < 5; j++)
                {
                    if ((i + j) == (X + Y) && ButtonArr[i, j].Content == sign)
                    { DigReverse++; }
                    if ((i - j) == (X - Y) && ButtonArr[i, j].Content == sign)
                    { DigMain++; }
                }
            }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (ButtonArr[i, j].Content == null)
                        tie *= 0;
                }

            if (tie == 1)
            {
                if (MessageBox.Show("Tie!" + "\nOK - retry, Cancel - Main menu.", "Tie", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    ResetTable();
                else
                {
                    MainWindow nwc = new MainWindow();
                    wn.Hide();
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
                    wn.Hide();
                    nwc.Show();
                }
                ResetTable();
            }

        }
        private void ResetTable()
        {
            Stats.Content = "Wins:\nO - " + winO + "\nX - " + winX;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    ButtonArr[i, j].Content = null;
                }
        }

    }
}

