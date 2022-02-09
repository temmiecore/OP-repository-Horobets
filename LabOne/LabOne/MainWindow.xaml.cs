using System;
using System.Windows;

namespace LabOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Win2_Click(object sender, RoutedEventArgs e)
        {
            Window2 nwc = new Window2();
            Hide();
            nwc.Show();
        }

        private void Win3_Click(object sender, RoutedEventArgs e)
        {
            Window3 nwc = new Window3();
            Hide();
            nwc.Show();
        }

        private void Win4_Click(object sender, RoutedEventArgs e)
        {
            Window4 nwc = new Window4();
            Hide();
            nwc.Show();
        }

        private void Win5_Click(object sender, RoutedEventArgs e)
        {
            Window5 nwc = new Window5();
            Hide();
            nwc.Show();
        }

        private void WinExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }
    }
}
