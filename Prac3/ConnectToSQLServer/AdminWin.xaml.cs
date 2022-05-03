using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConnectToSQLServer
{

    public partial class AdminWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlDataAdapter adapter;
        public AdminWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        string enterpass; int count = 0;
        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            enterpass = AdminPass.Password;
            string que = "SELECT * FROM MainTable WHERE Login = 'ADMIN';";
            connection = new SqlConnection(connectionString);
            connection.Open();
            adapter = new SqlDataAdapter(que, connection);
            DataTable dT = new DataTable();
            adapter.Fill(dT);
            if (enterpass == dT.Rows[0][3].ToString())
            {
                AdminMenuWin wn = new AdminMenuWin(enterpass);
                wn.Show();
                Close();
            }
            else
            {
                count++;
                MessageBox.Show("Incorrect password!");
                AdminPass.Password = "";
                if (count == 3)
                    Application.Current.Shutdown();
            }
            connection.Close();
        }

        
    }
}
