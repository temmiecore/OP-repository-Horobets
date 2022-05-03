using System.Windows;
using System.Configuration;
using System.Data.SqlClient;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;

        public MainWindow()
        {
            InitializeComponent();
            StartMethod();
        }

        private void StartMethod()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
            bool check;
            string que = "SELECT CASE WHEN OBJECT_ID('MainTable', 'U') IS NOT NULL THEN 1 ELSE 0 END";
            command = new SqlCommand(que, connection);
            check = (int)command.ExecuteScalar() == 1;
            if (check == false)
            {
                que = "CREATE TABLE MainTable (Name varchar(20), Surname varchar(20), Login varchar(30) PRIMARY KEY NOT NULL, "
                    + " Password nvarchar(20), Ban bit NOT NULL, PassRestr bit NOT NULL);";
                command = new SqlCommand(que, connection); command.ExecuteNonQuery();
                que = "INSERT INTO MainTable (Name, Surname, Login, Password, Ban, PassRestr) VALUES ('','','ADMIN','', 0, 0)";
                command = new SqlCommand(que, connection); command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void AdminMode_Click(object sender, RoutedEventArgs e)
        {
            AdminWin administration = new AdminWin();
            administration.Show();
            Close();
        }
        private void UserMode_Click(object sender, RoutedEventArgs e)
        {
            UserLoginWin userFormWPF = new UserLoginWin();
            userFormWPF.Show();
            Close();
        }
        private void AboutDev_Click(object sender, RoutedEventArgs e)
        {
            AboutWin devWindow = new AboutWin();
            devWindow.Show();
            Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}