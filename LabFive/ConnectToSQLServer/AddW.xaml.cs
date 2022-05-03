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
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConnectToSQLServer
{
    
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddW : Window
    {
        
        public AddW()
        {
            InitializeComponent();
        }

        private void AddAtt_Click(object sender, RoutedEventArgs e)
        {
            AddAttW wn = new AddAttW();
            wn.Show();
            Close();
        }

        private void AddCl_Click(object sender, RoutedEventArgs e)
        {
            AddClW wn = new AddClW();
            wn.Show();
            Close();
        }

        private void AddCase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddJour_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
