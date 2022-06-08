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
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            GetFirstQ();
            GetSecondQ();
            GetThirdQ();
            GetSeventhQ();
        }

        private void GetAndDhowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();
        }

        private void GetFirstQ()
        {
            string sqlQ = "SELECT Alias, Clients.Adress, Clients.PhoneNum, Crimetype " +
                          "FROM ClienTs " +
                          "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                          " INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                          "ORDER BY CrimeType; ";
            try
            {
                GetAndDhowData(sqlQ, FirstQ);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void GetSecondQ()
        {
            string sqlQ = "SELECT Alias AS [Name, Surname], Clients.BDate AS [Date of Birth], Clients.Adress, Clients.PhoneNum, Crimetype AS Crime " +
                          "FROM ClienTs " +
                          "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                          "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                          "WHERE ClienTs.BDate >= '2004-01-01' " +
                          "ORDER BY Alias; ";
            try
            {
                GetAndDhowData(sqlQ, SecondQ);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GetThirdQ()
        {
            string sqlQ = "SELECT Alias, Clients.Adress, Clients.PhoneNum, Crimetype AS Crime, SentenceW  + ' years' AS [Worst Possible Scenario] " +
                            "FROM ClienTs " +
                          "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                           "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                          "WHERE IsYr = 1 AND SentenceW >= 1 " +
                          "ORDER BY Alias;";
            try
            {
                GetAndDhowData(sqlQ, ThirdQ);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

       
        
        private void GetSeventhQ()
        {
            string sqlQ = "SELECT '$'+CONVERT(varchar,SUM(Fee))  AS [Total Fee Number] FROM Cases;";
            try
            {
                GetAndDhowData(sqlQ, SeventhQ);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            Close();
            win.Show();
        }
    }
}

