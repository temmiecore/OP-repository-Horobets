﻿using System;
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

namespace ConnectToSQLServer
{
    /// <summary>
    /// Interaction logic for AboutWin.xaml
    /// </summary>
    public partial class AboutWin : Window
    {
        public AboutWin()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow wn = new MainWindow();
            wn.Show();
            Close();
        }
    }
}
