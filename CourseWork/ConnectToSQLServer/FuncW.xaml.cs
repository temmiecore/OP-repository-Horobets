using System;
using System.Windows;

namespace ConnectToSQLServer
{
    public partial class FuncW : Window
    {
        public FuncW()
        {
            InitializeComponent();
        }

        private void CellsBtn_Click(object sender, RoutedEventArgs e)
        {
            CellW wn = new CellW();
            wn.Show();
            Close();
        }

        private void FeeBtn_Click(object sender, RoutedEventArgs e)
        {
            FeeW wn = new FeeW();
            wn.Show();
            Close();
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintW wn = new PrintW();
            wn.Show();
            Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wn = new MainWindow();
            wn.Show();
            Close();
        }

        private void AttHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            AttHistoryW wn = new AttHistoryW();
            wn.Show();
            Close();
        }
    }
}
