using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Controls;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
        }


       

        private void AddW_Click(object sender, RoutedEventArgs e)
        {
            AddW wn = new AddW();
            wn.Show();
            Close();
        }

        private void Queries_Click(object sender, RoutedEventArgs e)
        {
            QuerieW wn = new QuerieW();
            wn.Show();
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }
    }
}

