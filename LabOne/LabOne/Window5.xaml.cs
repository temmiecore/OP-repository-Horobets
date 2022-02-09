using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabOne
{
    /// <summary>
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
            About.Text = "About section\nГоробець Нікіта Олегович, 2022 р.н.\nГрупа КП-13, ФПМ, КПІ.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nwc = new MainWindow();
            Hide();
            nwc.Show();
        }
    }
}
