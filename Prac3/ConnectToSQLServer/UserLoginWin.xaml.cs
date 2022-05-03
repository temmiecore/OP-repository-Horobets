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
            L1.Visibility = Visibility.Hidden; L2.Visibility = Visibility.Hidden; L3.Visibility = Visibility.Hidden;
            ExitBtn.Visibility = Visibility.Hidden; RegBtn.Visibility = Visibility.Hidden; LogInBtn.Visibility = Visibility.Hidden;
            LoginTextBox.Visibility = Visibility.Hidden; LPasswordTextBox.Visibility = Visibility.Hidden;
            ExitBtn.IsEnabled = false; RegBtn.IsEnabled = false; LogInBtn.IsEnabled = false; LoginTextBox.IsEnabled = false;
            LPasswordTextBox.IsEnabled = false;

            L4.Visibility = Visibility.Visible; L5.Visibility = Visibility.Visible; L6.Visibility = Visibility.Visible;
            L7.Visibility = Visibility.Visible; L8.Visibility = Visibility.Visible; L9.Visibility = Visibility.Visible;
            RegName.Visibility = Visibility.Visible; RegSurn.Visibility = Visibility.Visible; RegLogin.Visibility = Visibility.Visible;
            RegistrBtn.Visibility = Visibility.Visible; RegistrExt.Visibility = Visibility.Visible; RegPass.Visibility = Visibility.Visible; RegPass2.Visibility = Visibility.Visible;
            RegName.IsEnabled = true; RegSurn.IsEnabled = true; RegLogin.IsEnabled = true;
            RegistrBtn.IsEnabled = true; RegistrExt.IsEnabled = true; RegPass.IsEnabled = true; RegPass2.IsEnabled = true;

            LoginTextBox.Text = ""; LPasswordTextBox.Password = "";
        }
        //Really need to redo this part... ^


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
            L1.Visibility = Visibility.Visible; L2.Visibility = Visibility.Visible; L3.Visibility = Visibility.Visible;
            ExitBtn.Visibility = Visibility.Visible; RegBtn.Visibility = Visibility.Visible; LogInBtn.Visibility = Visibility.Visible;
            LoginTextBox.Visibility = Visibility.Visible; LPasswordTextBox.Visibility = Visibility.Visible;
            ExitBtn.IsEnabled = true; RegBtn.IsEnabled = true; LogInBtn.IsEnabled = true; LoginTextBox.IsEnabled = true;
            LPasswordTextBox.IsEnabled = true;

            L4.Visibility = Visibility.Hidden; L5.Visibility = Visibility.Hidden; L6.Visibility = Visibility.Hidden;
            L7.Visibility = Visibility.Hidden; L8.Visibility = Visibility.Hidden; L9.Visibility = Visibility.Hidden;
            RegName.Visibility = Visibility.Hidden; RegSurn.Visibility = Visibility.Hidden; RegLogin.Visibility = Visibility.Hidden;
            RegistrBtn.Visibility = Visibility.Hidden; RegistrExt.Visibility = Visibility.Hidden; RegPass.Visibility = Visibility.Hidden; RegPass2.Visibility = Visibility.Hidden;
            RegName.IsEnabled = false; RegSurn.IsEnabled = false; RegLogin.IsEnabled = false;
            RegistrBtn.IsEnabled = false; RegistrExt.IsEnabled = false; RegPass.IsEnabled = false; RegPass2.IsEnabled = false;

            RegName.Text = ""; RegSurn.Text = ""; RegLogin.Text = ""; RegPass.Password = ""; RegPass2.Password = "";
        }
    }
}
