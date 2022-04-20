using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Linq;


namespace LabTwo._2
{
    class Window4
    {
        double num1 = 0, num2 = 0;
        string oper = "";
        Window wn = new Window();
        TextBlock ResultBox = new TextBlock();

        public Window4()
        {
            initControls();
        }

        private void initControls()
        {
            wn.Title = "Data Reader"; wn.Height = 500; wn.Width = 300; wn.Background = Brushes.Lavender;
            Grid grid = new Grid();
            grid.Width = 285; grid.Height = 470;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            RowDefinition[] rows = new RowDefinition[7];
            ColumnDefinition[] cols = new ColumnDefinition[4];
            for (int i = 0; i < 7; i++)
            {
                rows[i] = new RowDefinition();
                grid.RowDefinitions.Add(rows[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                cols[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(cols[i]);
            }

            int k = 1;
            for (int i = 4; i >= 2; i--)
                for (int j = 0; j < 3; j++)
                {
                    Button butt = new Button();
                    butt.Content = k; butt.FontSize = 24; butt.FontFamily = new FontFamily("SimSun-ExtB");
                    butt.Click += Button_Num;
                    Grid.SetRow(butt, i); Grid.SetColumn(butt, j);
                    grid.Children.Add(butt);
                    k++;
                }

            Button AC = new Button(); AC.Content = "AC"; AC.FontSize = 24; AC.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(AC, 1); Grid.SetColumn(AC, 0); AC.Click += AC_Click;
            Button Sign = new Button(); Sign.Content = "+/-"; Sign.FontSize = 24; Sign.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(Sign, 1); Grid.SetColumn(Sign, 1); Sign.Click += Button_Sign;
            Button Modulo = new Button(); Modulo.Content = "%"; Modulo.FontSize = 24; Modulo.FontFamily = new FontFamily("SimSun-ExtB"); Modulo.Tag = "Modulo";
            Grid.SetRow(Modulo, 1); Grid.SetColumn(Modulo, 2); Modulo.Click += Button_Op;
            Button Plus = new Button(); Plus.Content = "+"; Plus.FontSize = 24; Plus.FontFamily = new FontFamily("SimSun-ExtB"); Plus.Tag = "Add";
            Grid.SetRow(Plus, 5); Grid.SetColumn(Plus, 2); Grid.SetColumnSpan(Plus, 2);
            Button Minus = new Button(); Minus.Content = "-"; Minus.FontSize = 24; Minus.FontFamily = new FontFamily("SimSun-ExtB"); Minus.Tag = "Substract";
            Grid.SetRow(Minus, 4); Grid.SetColumn(Minus, 3); Minus.Click += Button_Op;
            Button Divide = new Button(); Divide.Content = ":"; Divide.FontSize = 24; Divide.FontFamily = new FontFamily("SimSun-ExtB");Divide.Tag = "Divide";
            Grid.SetRow(Divide, 3); Grid.SetColumn(Divide, 3); Divide.Click += Button_Op;
            Button Mult = new Button(); Mult.Content = "*"; Mult.FontSize = 24; Mult.FontFamily = new FontFamily("SimSun-ExtB"); Mult.Tag = "Multiply";
            Grid.SetRow(Mult, 2); Grid.SetColumn(Mult, 3); Mult.Click += Button_Op;
            Button Zero = new Button(); Zero.Content = 0; Zero.FontSize = 24; Zero.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(Zero, 5); Grid.SetColumn(Zero, 1); Zero.Click += Button_Num;
            Button Result = new Button(); Result.Content = "="; Result.FontSize = 24; Result.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(Result, 6); Grid.SetColumn(Result, 0); Grid.SetColumnSpan(Result, 2); Result.Click += Result_Click;
            Button Float = new Button(); Float.Content = ","; Float.FontSize = 24; Float.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(Float, 5); Grid.SetColumn(Float, 0); Float.Click += Button_Float;
            Button Sqrt = new Button(); Sqrt.Content = "sqrt"; Sqrt.FontSize = 24; Sqrt.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(Sqrt, 1); Grid.SetColumn(Sqrt, 3); Sqrt.Click += Button_Sqrt;
            Button tomain = new Button(); tomain.Content = "Main menu"; tomain.FontSize = 24; tomain.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(tomain, 6); Grid.SetColumn(tomain, 2); Grid.SetColumnSpan(tomain, 2); tomain.Click += _4ToMain_Click;

            ResultBox.FontSize = 48; ResultBox.FontFamily = new FontFamily("SimSun-ExtB"); ResultBox.TextAlignment = TextAlignment.Right; ResultBox.Text = "0";
            Grid.SetRow(ResultBox, 0); Grid.SetColumn(ResultBox, 0); Grid.SetColumnSpan(ResultBox, 4); 

            grid.Children.Add(AC); grid.Children.Add(Sign); grid.Children.Add(Modulo); grid.Children.Add(Plus); grid.Children.Add(Minus);
            grid.Children.Add(Sqrt); grid.Children.Add(tomain); grid.Children.Add(Zero); grid.Children.Add(Result); grid.Children.Add(Float);
            grid.Children.Add(Mult); grid.Children.Add(Divide); grid.Children.Add(ResultBox);

            wn.Content = grid;
            wn.Show();
        }

        private void Button_Num(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (ResultBox.Text != "0")
                ResultBox.Text = $"{ResultBox.Text}{btn.Content}";
            else
                ResultBox.Text = $"{btn.Content}";
        }
        private void _4ToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nwc = new MainWindow();
            wn.Hide();
            nwc.Show();
        }

        private void AC_Click(object sender, RoutedEventArgs e)
        {
            num1 = 0;
            num2 = 0;
            oper = "";
            ResultBox.Text = "0";
        }

        private void Button_Float(object sender, RoutedEventArgs e)
        {
            if (ResultBox.Text.Length == 0)
                ResultBox.Text = "0,";
            else
                ResultBox.Text += ",";
        }

        private void Button_Op(object sender, RoutedEventArgs e)
        {
            num1 = double.Parse(ResultBox.Text);
            var btn = sender as Button;
            switch (btn.Tag)
            {
                case "Add":
                    { oper = "+"; ResultBox.Text = "0"; break; }
                case "Substract":
                    { oper = "-"; ResultBox.Text = "0"; break; }
                case "Divide":
                    { oper = "/"; ResultBox.Text = "0"; break; }
                case "Multiply":
                    { oper = "*"; ResultBox.Text = "0"; break; }
                case "Modulo":
                    { oper = "%"; ResultBox.Text = "0"; break; }
            }
        }

        private void Button_Sign(object sender, RoutedEventArgs e)
        {
            ResultBox.Text = (double.Parse(ResultBox.Text) * -1).ToString();
        }

        private void Button_Sqrt(object sender, RoutedEventArgs e)
        {
            num1 = double.Parse(ResultBox.Text);
            num1 = Math.Round(Math.Sqrt(num1), 4);
            ResultBox.Text = num1.ToString();
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            num2 = double.Parse(ResultBox.Text);
           
            switch (oper)
            {
                case "+": { ResultBox.Text = Math.Round((num1 + num2), 4).ToString(); break; }
                case "-": { ResultBox.Text = Math.Round((num1 - num2), 4).ToString(); break; }
                case "/": { ResultBox.Text = Math.Round((num1 / num2), 4).ToString(); break; }
                case "*": { ResultBox.Text = Math.Round((num1 * num2), 4).ToString(); break; }
                case "%": { ResultBox.Text = Math.Round((num1 % num2), 4).ToString(); break; }
            }
        }



    }
}