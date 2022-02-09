using System;
using System.Windows;
using System.Windows.Controls;

namespace LabOne
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {

        static double num1 = 0, num2 = 0;
        static string oper = "";
        public Window4()
        {
            InitializeComponent();
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
            Hide();
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
                    { oper = "+"; ResultBox.Text = "0"; Operand.Text = "+"; break; }
                case "Substract":
                    { oper = "-"; ResultBox.Text = "0"; Operand.Text = "-"; break; }
                case "Divide":
                    { oper = "/"; ResultBox.Text = "0"; Operand.Text = "/"; break; }
                case "Multiply":
                    { oper = "*"; ResultBox.Text = "0"; Operand.Text = "*"; break; }
                case "Modulo": //what "%" does i'm too scared to ask
                    { oper = "%"; ResultBox.Text = "0"; Operand.Text = "%"; break; }
            }
        }

        private void Button_Sign(object sender, RoutedEventArgs e)
        {
            ResultBox.Text = (double.Parse(ResultBox.Text) * -1).ToString();
        }

        private void Button_Sqrt(object sender, RoutedEventArgs e)
        {
            num1 = double.Parse(ResultBox.Text);
            num1 = Math.Round(Math.Sqrt(num1),4);
            ResultBox.Text = num1.ToString();
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            num2 = double.Parse(ResultBox.Text);
            Operand.Text = "";
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
