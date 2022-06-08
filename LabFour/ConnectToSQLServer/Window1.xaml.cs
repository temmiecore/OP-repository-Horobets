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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            GetFifthQ();
            GetFourthQ();
            GetSixthQ();
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
        private void GetFifthQ()
        {
            string sqlQ = "SELECT Journal.CaseID, Attorneys.NameSurname AS Attorney, Attorneys.PhoneNum AS [Attorney's Phone Number], " +
      " Alias AS Client, Clients.PhoneNum AS[Clien's Phone Number],   CASE IsYr  WHEN 1 THEN SentenceW + ' years' WHEN 0 THEN '$' + SentenceW END " +
 " AS[Worst Scenario],  CASE IsYr  WHEN 1 THEN SentenceGot + ' years' WHEN 0 THEN '$' + SentenceGot END  AS Result, '$' + CONVERT(varchar, Fee) " +
"FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
 "INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE SentenceGot < SentenceW ORDER BY Journal.CaseID; ";
            try
            {
                GetAndDhowData(sqlQ, FifthQ);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetSixthQ()
        {
            string sqlQ = "SELECT Attorneys.NameSurname AS [Name, Surname], Attorneys.Adress, Attorneys.PhoneNum AS [Phone Number], Education, " +
 "Clients.Alias[Client], Journal.CaseClosed AS[Closing Date], '$' + CONVERT(varchar, Fee) AS[Earned Fee] FROM Attorneys  INNER JOIN Journal ON Journal.AttID = Attorneys.AttID " +
"INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID WHERE IsOver = 1 ORDER BY Attorneys.NameSurname;  ";
            try
            {
                GetAndDhowData(sqlQ, SixthQ);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetFourthQ()
        {
            string sqlQ = "SELECT Journal.CaseID, Attorneys.NameSurname AS Attorney, Attorneys.PhoneNum AS[Attorney's Phone Number], " +
                       " Alias AS Client, Clients.PhoneNum AS[Clien's Phone Number], CrimeType AS Crime, " +
                      " CaseClosed AS[Case closing Date],   CASE IsYr    WHEN 1 THEN SentenceGot + ' years'  WHEN 0 THEN '$' + SentenceGot END " +
                       "  AS Result, '$' + CONVERT(varchar, Fee) FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID  INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                          "  INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE IsOver = 1 ORDER BY Journal.CaseID; ";
            try
            {
                GetAndDhowData(sqlQ, FourthQ);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
