using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ConnectToSQLServer
{
    /// <summary>
    /// Interaction logic for UserLoginWin.xaml
    /// </summary>
    public partial class UserLoginWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlDataAdapter adapter;
        public UserLoginWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        //Login Part Logic
        string LogLogin, LogPass; int count = 0;
        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check if login is in DB
            //If it is, check password
            //If password is correct, open clients window
            //...
            //If login is not in DB, messagebox("Login is not in DB. Do you want to register (Y) or try entering login again (N)?") or smth
            //If password not correct, count++ and on count = 3 message and exit window

            LogLogin = LoginTextBox.Text;
            LogPass = LPasswordTextBox.Password;

            connection = new SqlConnection(connectionString);
            connection.Open();
            string que = "SELECT * FROM MainTable WHERE Login = '" + LogLogin + "';";
            adapter = new SqlDataAdapter(que, connection);
            DataTable dT = new DataTable();
            adapter.Fill(dT);
            if (dT.Rows.Count == 0)
            {
                MessageBoxResult mbr = MessageBox.Show("User is not registered. Register now?", "Incorrect login", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.Yes)
                { RegLogin.Text = LoginTextBox.Text; RegOpen();  }
                else
                    LPasswordTextBox.Password = "";
            }
            else
            {
                bool IsBanned = Convert.ToBoolean(dT.Rows[0][4]);
                if (IsBanned)
                    MessageBox.Show("User is banned by administration!");
                else
                {
                    if (dT.Rows[0][3].ToString() == LogPass)
                    {
                        LoginTextBox.Text = ""; LPasswordTextBox.Password = "";
                        count = 0;
                        UserConfigWin wn = new UserConfigWin(LogLogin);
                        wn.Show();
                        Close();
                    }
                    else
                    {
                        count++;
                        MessageBox.Show("Incorrect password!");
                        LPasswordTextBox.Password = "";
                        if (count == 3)
                            Application.Current.Shutdown();
                    }
                }
            }
            connection.Close();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wn = new MainWindow();
            wn.Show();
            Close();
        }
        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegOpen();
        }
        private void RegOpen()
        {
            LoginGrid.Visibility = Visibility.Collapsed;
            LoginGrid.IsEnabled = false;
            RegGrid.Visibility = Visibility.Visible;
            RegGrid.IsEnabled = true;
            LoginTextBox.Text = ""; LPasswordTextBox.Password = "";
        }


        //Registration Part Logic
        string RName, RSurname, RLogin, RPassword, RPasswordCheck;
        private void RegistrBtn_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            RName = RegName.Text;
            RSurname = RegSurn.Text;
            RLogin = RegLogin.Text;
            RPassword = RegPass.Password;
            RPasswordCheck = RegPass2.Password;

            string que = "";
            if (connection.State == ConnectionState.Open)
            {
                try
                {
                    if ((RPassword == RPasswordCheck) && (RLogin != "") && (RPassword != "") && IsPassCorrect(RPassword))
                    {
                        que = "INSERT INTO MainTable (Name, Surname, Login, Password, Ban, PassRestr) VALUES " +
                            "('" + RName + "', '" + RSurname + "', '" + RLogin + "', '" + RPassword + "', 0, 0);";
                        SqlCommand comm = new SqlCommand(que, connection);
                        comm.ExecuteNonQuery();
                        MessageBox.Show("User succesfully registered!");
                        RegName.Text = ""; RegSurn.Text = ""; RegLogin.Text = ""; RegPass.Password = ""; RegPass2.Password = "";
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong with password! Check if password has one lower/upper letter, digit and has no spaces!");
                        RegPass.Password = ""; RegPass2.Password = "";
                    }
                }
                catch
                {
                    MessageBox.Show("User already exists!");
                }
            }
            connection.Close();
        }
        private bool IsPassCorrect(string password)
        {
            Regex pattern = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,8}$");
            return pattern.IsMatch(password);
            //Password expresion that requires one lower case letter, one upper case letter, one digit, 6-13 length, and no spaces.
        }
        private void RegistrExt_Click(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Visible;
            LoginGrid.IsEnabled = true;
            RegGrid.Visibility = Visibility.Collapsed;
            RegGrid.IsEnabled = false;
            RegName.Text = ""; RegSurn.Text = ""; RegLogin.Text = ""; RegPass.Password = ""; RegPass2.Password = "";
        }
    }
}
