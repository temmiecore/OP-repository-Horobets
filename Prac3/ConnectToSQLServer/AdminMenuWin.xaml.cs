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

    public partial class AdminMenuWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlDataAdapter adapter;
        DataTable dT;

        int usersCount;
        string AdminPass;
        public AdminMenuWin(string AdminPassword)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            AdminPass = AdminPassword;
            UpdateDataTable();
        }



        //Change admin password
        private void PassChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string currp = PassCurrBox.Password.ToString();
            string newp = PassNewBox.Password.ToString();
            string newpcheck = PassNewCheckBox.Password.ToString();
            if ((currp == AdminPass) && (newp != "") && (newp == newpcheck))
            {
                if (connection.State == ConnectionState.Open)
                {
                    string que = "UPDATE MainTable SET Password ='" + newp + "' WHERE Login = 'ADMIN'; ";
                    SqlCommand command = new SqlCommand(que, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Password changed!");
                    PassCurrBox.Password = ""; PassNewBox.Password = ""; PassNewCheckBox.Password = "";
                    UpdateDataTable();
                }
            }
            connection.Close();
        }


        //User menu logic
        int userNum=0;
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            userNum--;
            if (userNum < 0)
                userNum = usersCount-1;
            Namelabel.Content = dT.Rows[userNum][0].ToString();
            Surlabel.Content = dT.Rows[userNum][1].ToString();
            Loglabel.Content = dT.Rows[userNum][2].ToString();
            Banlabel.Content = dT.Rows[userNum][3].ToString();
            Restrlabel.Content = dT.Rows[userNum][4].ToString();
          /*  ClientnumBox.SelectedItem = dT.Rows[userNum][2].ToString();*/
        }
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            userNum++;
            if (userNum > 6)
                userNum = 0;
            Namelabel.Content = dT.Rows[userNum][0].ToString();
            Surlabel.Content = dT.Rows[userNum][1].ToString();
            Loglabel.Content = dT.Rows[userNum][2].ToString();
            Banlabel.Content = dT.Rows[userNum][3].ToString();
            Restrlabel.Content = dT.Rows[userNum][4].ToString();
            /*ClientnumBox.SelectedItem = dT.Rows[userNum][2].ToString();*/
        }
        private void ClientnumBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Selection = (e.AddedItems[0]).ToString();

            connection = new SqlConnection(connectionString);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                adapter = new SqlDataAdapter("SELECT Name, Surname, Login, Ban, PassRestr FROM MainTable WHERE Login = '" + Selection + "';", connection);
                dT = new DataTable();
                adapter.Fill(dT);
                Namelabel.Content = dT.Rows[0][0].ToString();
                Surlabel.Content = dT.Rows[0][1].ToString();
                Loglabel.Content = dT.Rows[0][2].ToString();
                Banlabel.Content = dT.Rows[0][3].ToString();
                Restrlabel.Content = dT.Rows[0][4].ToString();
                UpdateDataTable();
            }
            connection.Close();
        }



        //Change checkboxes
        private void BanBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            bool UserStatus = (bool)BanCheck.IsChecked;
            if (connection.State == ConnectionState.Open)
            {
                string que = "UPDATE MainTable SET Ban ='" + UserStatus + "' WHERE Login='" +
                Loglabel.Content.ToString() + "';";
                SqlCommand command = new SqlCommand(que, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            UpdateDataTable();
        }
        private void RestrBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            String strQ;
            bool UserRestriction = (bool)RestrCheck.IsChecked;
            if (connection.State == ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET PassRestr ='" + UserRestriction +
                       "' WHERE Login = '" + Loglabel.Content.ToString() + "'; ";
                SqlCommand command = new SqlCommand(strQ, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            UpdateDataTable();
        }


        //Add user
        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    if (ClientNewBox.Text != "")
                    {
                        string que = "INSERT INTO MainTable (Name, Surname, Login, Password, Ban, PassRestr) values('', '', '" + ClientNewBox.Text + "', '', 1, 0); ";
                        SqlCommand command = new SqlCommand(que, connection);
                        command.ExecuteNonQuery();
                        usersCount++;
                    }
                    else
                        MessageBox.Show("Enter user's login!");
                }
                UpdateDataTable();
            }
            catch
            {
                MessageBox.Show("User with this login already exists!");
            }
            connection.Close();
        }


        //Helpers
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wn = new MainWindow();
            wn.Show();
            Close();
        }
        private void UpdateDataTable()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                adapter = new SqlDataAdapter("SELECT Name, Surname, Login, Ban, PassRestr FROM MainTable", connection);
                dT = new DataTable("Користувачі системи");
                adapter.Fill(dT);
                MainGrid.ItemsSource = dT.DefaultView;
                usersCount = dT.Rows.Count;
                ClientnumBox.SelectionChanged -= ClientnumBox_SelectionChanged;
                ClientnumBox.Items.Clear();
                for (int i = 0; i < usersCount; i++)
                    ClientnumBox.Items.Add(dT.Rows[i][2]);
                ClientnumBox.SelectionChanged += ClientnumBox_SelectionChanged;
            }
            connection.Close();
        }
       
    }
}
