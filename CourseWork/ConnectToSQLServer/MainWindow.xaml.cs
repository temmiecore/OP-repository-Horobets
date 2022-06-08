using System.Windows;

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
            AddAttW wn = new AddAttW();
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

        private void Func_Click(object sender, RoutedEventArgs e)
        {
            FuncW wn = new FuncW();
            wn.Show();
            Close();
        }
    }
}

