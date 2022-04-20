using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabTwo._2
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
        }

        private void Win3_Click(object sender, RoutedEventArgs e)
        {
            Window3 nwc = new Window3();
            Hide();
        }

        private void Win4_Click(object sender, RoutedEventArgs e)
        {
            Window4 nwc = new Window4();
            Hide();
        }

        private void Win5_Click(object sender, RoutedEventArgs e)
        {
            Window5 nwc = new Window5();
            Hide();
        }

        private void WinExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }
    }
}

