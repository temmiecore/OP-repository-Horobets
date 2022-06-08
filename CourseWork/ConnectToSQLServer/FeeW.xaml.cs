using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace ConnectToSQLServer
{
    public partial class FeeW : Window
    {
        string que;

        string connectionString = null;
        SqlConnection connection = null;
        public FeeW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void OneAttBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != "" && int.TryParse(IDBox.Text, out int ID))
            {
                que = "SELECT SUM(Fee) FROM Cases INNER JOIN Journal ON Journal.CaseID = Cases.CaseID INNER JOIN Attorneys ON Journal.AttID = Attorneys.AttID WHERE Journal.AttID = " + ID;
                connection = new SqlConnection(connectionString);
                connection.Open();
                FeeLabel.Content = "$" + new SqlCommand(que, connection).ExecuteScalar();
                connection.Close();
            }
            else
                System.Windows.MessageBox.Show("Введіть правильне ID адвоката!");
        }

        private void AllAttBtn_Click(object sender, RoutedEventArgs e)
        {
            que = "SELECT SUM(Fee) FROM Cases INNER JOIN Journal ON Journal.CaseID = Cases.CaseID INNER JOIN Attorneys ON Journal.AttID = Attorneys.AttID WHERE IsOver = 0";
            connection = new SqlConnection(connectionString);
            connection.Open();
            FeeLabel.Content = "$" + new SqlCommand(que, connection).ExecuteScalar();
            connection.Close();
        }

        private void AllCasesBtn_Click(object sender, RoutedEventArgs e)
        {
            que = "SELECT SUM(Fee) FROM Cases INNER JOIN Journal ON Journal.CaseID = Cases.CaseID INNER JOIN Attorneys ON Journal.AttID = Attorneys.AttID";
            connection = new SqlConnection(connectionString);
            connection.Open();
            FeeLabel.Content = "$" + new SqlCommand(que, connection).ExecuteScalar();
            connection.Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            FuncW wn = new FuncW();
            wn.Show();
            Close();
        }
        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Fee";
            sf.Filter = "Text file (*.txt)|*.txt";
            DialogResult result = sf.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(sf.FileName + ".txt", FeeLabel.Content.ToString());
                System.Windows.MessageBox.Show("Чек збережено!");
            }
        }
    }
}
