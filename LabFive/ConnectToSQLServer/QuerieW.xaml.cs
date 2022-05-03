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

    public partial class QuerieW : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        string[] queries;
        int querieNum = 0;
        public QuerieW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            queries = new string[7];
            queries[0] = "SELECT Alias, Clients.Adress, Clients.PhoneNum, Crimetype " +
                          "FROM ClienTs " +
                          "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                          " INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                          "ORDER BY CrimeType; ";

            queries[1] = "SELECT Alias AS [Name, Surname], Clients.BDate AS [Date of Birth], Clients.Adress, Clients.PhoneNum, Crimetype AS Crime " +
                                       "FROM ClienTs " +
                                       "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                                       "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                       "WHERE ClienTs.BDate >= '2004-01-01' " +
                                       "ORDER BY Alias; ";

            queries[2] = "SELECT Alias, Clients.Adress, Clients.PhoneNum, Crimetype AS Crime, SentenceW  + ' years' AS [Worst Possible Scenario] " +
                                          "FROM ClienTs " +
                                        "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                                         "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                        "WHERE IsYr = 1 AND SentenceW >= 1 " +
                                        "ORDER BY Alias;";

            queries[3] = "SELECT Journal.CaseID, Attorneys.NameSurname AS Attorney, Attorneys.PhoneNum AS[Attorney's Phone Number], " +
                                     " Alias AS Client, Clients.PhoneNum AS[Clien's Phone Number], CrimeType AS Crime, " +
                                    " CaseClosed AS[Case closing Date],   CASE IsYr    WHEN 1 THEN SentenceGot + ' years'  WHEN 0 THEN '$' + SentenceGot END " +
                                     "  AS Result, Fee FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID  INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                        "  INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE IsOver = 1 ORDER BY Journal.CaseID; ";

            queries[4] = "SELECT Journal.CaseID, Attorneys.NameSurname AS Attorney, Attorneys.PhoneNum AS [Attorney's Phone Number], " +
                     " Alias AS Client, Clients.PhoneNum AS[Clien's Phone Number],   CASE IsYr  WHEN 1 THEN SentenceW + ' years' WHEN 0 THEN '$' + SentenceW END " +
                " AS[Worst Scenario],  CASE IsYr  WHEN 1 THEN SentenceGot + ' years' WHEN 0 THEN '$' + SentenceGot END  AS Result, Fee " +
               "FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                "INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE SentenceGot < SentenceW ORDER BY Journal.CaseID; ";

            queries[5] = "SELECT Attorneys.NameSurname AS [Name, Surname], Attorneys.Adress, Attorneys.PhoneNum AS [Phone Number], Education, " +
                "Clients.Alias[Client], Journal.CaseClosed AS[Closing Date], Fee AS[Earned Fee] FROM Attorneys  INNER JOIN Journal ON Journal.AttID = Attorneys.AttID " +
               "INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID WHERE IsOver = 1 ORDER BY Attorneys.NameSurname;  ";

            queries[6] = "SELECT SUM(Fee) AS [Total Fee Number] FROM Cases;";

            ShowQuerie(querieNum);
            Label.Content = "№1, Усі підзахисні";
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            querieNum = (querieNum + 1) % 7;
            switch (querieNum)
            {
                case 0: { Label.Content = "№1, Усі підзахисні"; break; }
                case 1: { Label.Content = "№2, Неповнолітні"; break; }
                case 2: { Label.Content = "№3, Справи зі сроком > 1 року"; break; }
                case 3: { Label.Content = "№4, Завершені справи"; break; }
                case 4: { Label.Content = "№5, Ефективні справи"; break; }
                case 5: { Label.Content = "№6, Адвокати з завершеними справами"; break; }
                case 6: { Label.Content = "№7, Чек на оплату"; break; }
            }
            ShowQuerie(querieNum);
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            querieNum = (querieNum - 1) % 7;
            if (querieNum < 0)
                querieNum = 6;
            switch (querieNum)
            {
                case 0: { Label.Content = "№1, Усі підзахисні"; break; }
                case 1: { Label.Content = "№2, Неповнолітні"; break; }
                case 2: { Label.Content = "№3, Справи зі сроком > 1 року"; break; }
                case 3: { Label.Content = "№4, Незавершені справи"; break; }
                case 4: { Label.Content = "№5, Ефективні справи"; break; }
                case 5: { Label.Content = "№6, Адвокати з завершеними справами"; break; }
                case 6: { Label.Content = "№7, Чек на оплату"; break; }
            }
            ShowQuerie(querieNum);
        }

        private void ShowQuerie(int num)
        {
            string que = queries[num];
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(que, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            InfoGrid.ItemsSource = Table.DefaultView;
            connection.Close();
        }
    }
}
