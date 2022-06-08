using System;
using System.Windows;
using System.Windows.Controls;
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
            queries = new string[6];
            queries[0] = "SELECT Alias AS [Псевдонім], Clients.Adress AS [Адреса], Clients.PhoneNum AS [Номер телефону]" +
                          "FROM ClienTs " +
                          "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                          " INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                          "ORDER BY CrimeType; ";

            queries[1] = "SELECT Alias AS [Псевдонім], CONVERT(Date, Clients.BDate) AS [День народження], Clients.Adress  AS [Адреса], Clients.PhoneNum AS [Номер телефону], Crimetype AS [Правопорушення]" +
                                       "FROM ClienTs " +
                                       "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                                       "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                       "WHERE ClienTs.BDate >= '2004-01-01' " +
                                       "ORDER BY Alias; ";

            queries[2] = "SELECT Alias AS [Псевдонім], Clients.Adress  AS [Адреса], Clients.PhoneNum AS [Номер телефону], Crimetype AS [Правопорушення], SentenceW  + ' years' AS [Найгірший випадок] " +
                                          "FROM ClienTs " +
                                        "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                                         "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                        "WHERE IsYr = 1 AND SentenceW >= 1 " +
                                        "ORDER BY Alias;";

            queries[3] = "SELECT Journal.CaseID AS [Номер справи], Attorneys.NameSurname AS [Адвокат], Attorneys.PhoneNum AS [Номер телефону адвоката], " +
                                     " Alias AS [Клієнт], Clients.PhoneNum AS [Номер телефону клієнта], CrimeType AS [Правопорушення], " +
                                    " CaseStart AS [Початок справи], " +
                                     " '$' + CONVERT(varchar, Fee) AS [Гонорар] FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID  INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                        "  INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE IsOver = 0 ORDER BY Journal.CaseID; ";

            queries[4] = "SELECT Journal.CaseID AS [Номер справи], Attorneys.NameSurname AS [Адвокат], Attorneys.PhoneNum AS [Номер телефону адвоката], " +
                     " Alias AS [Клієнт], Clients.PhoneNum AS [Номер телефону клієнта],   CASE IsYr  WHEN 1 THEN SentenceW + ' years' WHEN 0 THEN '$' + SentenceW END " +
                " AS[Найгірший випадок],  CASE IsYr  WHEN 1 THEN SentenceGot + ' years' WHEN 0 THEN '$' + SentenceGot END  AS [Результат], '$' + CONVERT(varchar, Fee) AS [Гонорар адвоката] " +
               "FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                "INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE SentenceGot < SentenceW ORDER BY Journal.CaseID; ";

            queries[5] = "SELECT Journal.CaseID AS [Номер справи], Attorneys.NameSurname AS [Адвокат], Attorneys.PhoneNum AS [Номер телефону адвоката], " +
                      " Alias AS [Клієнт], Clients.PhoneNum AS [Номер телефону клієнта],   CASE IsYr  WHEN 1 THEN SentenceB + ' years' WHEN 0 THEN '$' + SentenceB END " +
                " AS[Найкращий випадок], CASE IsYr  WHEN 1 THEN SentenceGot + ' years' WHEN 0 THEN '$' + SentenceGot END  AS  [Результат], '$' + CONVERT(varchar, Fee) AS [Гонорар адвоката] " +
              " FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID" +
                " INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE SentenceGot > SentenceB ORDER BY Journal.CaseID;";

            ShowQuerie(querieNum);
            Label.Content = "№1, Усі підзахисні";
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            querieNum = (querieNum + 1) % 6;
            switch (querieNum)
            {
                case 0: { Label.Content = "№1, Усі підзахисні"; break; }
                case 1: { Label.Content = "№2, Неповнолітні підзахисні"; break; }
                case 2: { Label.Content = "№3, Справи зі сроком > 1 року"; break; }
                case 3: { Label.Content = "№4, Незавершені справи"; break; }
                case 4: { Label.Content = "№5, Ефективні справи"; break; }
                case 5: { Label.Content = "№6, Неефективні справи"; break; }
            }
            ShowQuerie(querieNum);
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            querieNum = (querieNum - 1) % 6;
            if (querieNum < 0)
                querieNum = 5;
            switch (querieNum)
            {
                case 0: { Label.Content = "№1, Усі підзахисні"; break; }
                case 1: { Label.Content = "№2, Неповнолітні"; break; }
                case 2: { Label.Content = "№3, Справи зі сроком > 1 року"; break; }
                case 3: { Label.Content = "№4, Незавершені справи"; break; }
                case 4: { Label.Content = "№5, Ефективні справи"; break; }
                case 5: { Label.Content = "№6, Неефективні справи"; break; }
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
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow wn = new MainWindow();
            wn.Show();
        }
    }
}
