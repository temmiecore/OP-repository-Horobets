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
using System.Text.RegularExpressions;

namespace ConnectToSQLServer
{
    /// <summary>
    /// Interaction logic for UserConfigWin.xaml
    /// </summary>
    public partial class UserConfigWin : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlDataAdapter adapter;
        string Login;
        public UserConfigWin(string log)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
          
            Login = log;
        }


        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] info = new string[4];
            string[] columns = new string[3] { "Name", "Surname", "Password" };
            info[0] = Name.Text; info[1] = Surname.Text; info[2] = NewPass.Password; info[3] = NewPassCheck.Password;
            connection = new SqlConnection(connectionString);
            connection.Open();
            string que = "SELECT * FROM MainTable WHERE Login = '" + Login + "';";
            adapter = new SqlDataAdapter(que, connection);
            DataTable dT = new DataTable();
            adapter.Fill(dT);

            if (connection.State == System.Data.ConnectionState.Open)
            {
                que = "";
                if (info[0] == "" && info[1] == "" && info[2] == "" && info[3] == "")
                    MessageBox.Show("Input information to update!");
                else
                {
                    if ((info[2] == info[3]) && (info[2] != ""))
                    {
                        if (Convert.ToBoolean(dT.Rows[0][5]) == true)
                        {
                            if (IsPassCorrect(info[3]))
                            {
                                que = "UPDATE MainTable SET";
                                for (int i = 0; i < 3; i++)
                                {
                                    if (info[i] != "")
                                        que += " " + columns[i] + " = " + info[i] + ", ";
                                }
                                que = que.Remove(que.Length - 2);
                                que += " WHERE Login='" + Login + "';";
                               
                                SqlCommand command = new SqlCommand(que, connection);
                                command.ExecuteNonQuery();
                                MessageBox.Show("User info Updated!");
                                Name.Text = ""; Surname.Text = ""; NewPass.Password = ""; NewPassCheck.Password = "";
                            }
                            else
                                MessageBox.Show("Password requires one lower case letter, one upper case letter, one digit, 6-13 length, and no spaces.");

                        }
                        else
                        {
                            que = "UPDATE MainTable SET";
                            for (int i = 0; i < 3; i++)
                            {
                                if (info[i] != "")
                                    que += " " + columns[i] + " = '" + info[i] + "', ";
                            }
                            que = que.Remove(que.Length - 2);
                            que += " WHERE Login='" + Login + "';";
                           
                            SqlCommand command = new SqlCommand(que, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("User info Updated!");
                            Name.Text = ""; Surname.Text = ""; NewPass.Password = ""; NewPassCheck.Password = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введено пустий пароль або новий пароль повторно введено некоректно!");
                    }
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


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow wn = new MainWindow();
            wn.Show();
        }
    }
}
