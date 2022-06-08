using System;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConnectToSQLServer
{
    public partial class PrintW : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        string que;
        DataTable dT = null;

        public PrintW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void PickBtn_Click(object sender, RoutedEventArgs e)
        {
            int selected = PrintItems.SelectedIndex;
            switch (selected)
            {
                case -1: { System.Windows.MessageBox.Show("Оберіть таблицю для друку!"); break; }
                case 0:
                    {
                        que = "SELECT TOP(100) PERCENT ATTID AS ID, NameSurname AS Alias, PhoneNum AS[Phone Number], Adress, CASE WHEN Education IS NULL THEN 'No education / NI' ELSE Education END AS Education, BDate AS Birthday " +
                                 "FROM dbo.Attorneys " +
                                    "ORDER BY ID; ";
                        break;
                    }
                case 1:
                    {
                        que = "SELECT CID AS ID, Alias, Adress, PhoneNum AS [Phone Number], CASE WHEN IsPhys = 1 THEN CONVERT(varchar, BDate) ELSE 'Jur. Client' END AS [Birthday Date] " +
                                  " FROM dbo.ClienTs" +
                                      " ORDER BY ID; ";
                        break;
                    }
                case 2:
                    {
                        que = "SELECT TOP (100) PERCENT Journal.CaseID AS [Номер справи],'#'+ CONVERT(varchar, Attorneys.AttID) + ', ' + Attorneys.NameSurname AS Адвокат, '#'+ CONVERT(varchar,Clients.CID) + ', ' + ClienTs.Alias AS Клієнт, Journal.CaseStart AS [Дата початку], CASE WHEN IsOver = 1 "+
                                    " THEN 'Закінчена' ELSE 'Не закінчена' END AS [Справа закінчена?],   ISNULL(CASE WHEN IsYr = 1 THEN SentenceGot + ' p.' ELSE '$' + SentenceGot END, 'NI')AS  [Результат - вирок]" +
                                     " , ISNULL(CONVERT(varchar,CaseClosed), 'NI') AS [Дата закінчення], ISNULL(CASE WHEN IsYr = 0 THEN 'Вирок - штраф' ELSE PrisonName END, 'NI') AS Тюрьма, ISNULL(CASE WHEN IsYr = 0 THEN 'Вирок - штраф' ELSE '№ '+CONVERT(varchar, CellNum) END, 'NI') AS [Номер камери]" +
                                      " FROM     Journal INNER JOIN Attorneys ON Journal.AttID = Attorneys.AttID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID INNER JOIN ClienTs ON Journal.ClD = ClienTs.CID" +
                                      " ORDER BY Journal.CaseID;";
                        break;
                    }
                case 3:
                    {
                        que = "SELECT Alias, Clients.Adress, Clients.PhoneNum AS [Phone Number], Crimetype AS Crime " +
                               "FROM ClienTs " +
                               "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                               " INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                               "ORDER BY CrimeType; ";
                        break;
                    }
                case 4:
                    {
                        que = "SELECT Alias AS [Name, Surname], CONVERT(Date, Clients.BDate) AS [Date of Birth], Clients.Adress , Clients.PhoneNum AS [Phone Number], Crimetype AS Crime " +
                                       "FROM ClienTs " +
                                       "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                                       "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                       "WHERE ClienTs.BDate >= '2004-01-01' " +
                                       "ORDER BY Alias; ";
                        break;
                    }
                case 5:
                    {
                        que = "SELECT Alias, Clients.Adress, Clients.PhoneNum AS [Phone Number], Crimetype AS Crime, SentenceW  + ' years' AS [Worst Possible Scenario] " +
                                          "FROM ClienTs " +
                                        "INNER JOIN Journal ON Journal.ClD = Clients.CID " +
                                         "INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                        "WHERE IsYr = 1 AND SentenceW >= 1 " +
                                        "ORDER BY Alias;";
                        break;
                    }
                case 6:
                    {
                        que = "SELECT Journal.CaseID, Attorneys.NameSurname AS Attorney, Attorneys.PhoneNum AS[Attorney's Phone Number], " +
                                     " Alias AS Client, Clients.PhoneNum AS[Clien's Phone Number], CrimeType AS Crime, " +
                                     " CaseStart AS[Case starting Date], " +
                                     " '$' + CONVERT(varchar, Fee) AS Fee FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID  INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                     "  INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE IsOver = 0 ORDER BY Journal.CaseID; ";
                        break;
                    }
                case 7:
                    {
                        que = "SELECT Journal.CaseID, Attorneys.NameSurname AS Attorney, Attorneys.PhoneNum AS [Attorney's Phone Number], " +
                                     " Alias AS Client, Clients.PhoneNum AS[Clien's Phone Number],   CASE IsYr  WHEN 1 THEN SentenceW + ' years' WHEN 0 THEN '$' + SentenceW END " +
                                     " AS[Worst Scenario],  CASE IsYr  WHEN 1 THEN SentenceGot + ' years' WHEN 0 THEN '$' + SentenceGot END  AS Result, '$' + CONVERT(varchar, Fee) AS Fee " +
                                     "FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID " +
                                      "INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE SentenceGot < SentenceW ORDER BY Journal.CaseID; ";
                        break;
                    }
                case 8:
                    {
                        que = "SELECT Journal.CaseID, Attorneys.NameSurname AS Attorney, Attorneys.PhoneNum AS[Attorney's Phone Number], " +
                                     "Alias AS Client, Clients.PhoneNum AS[Clien's Phone Number],   CASE IsYr  WHEN 1 THEN SentenceB + ' years' WHEN 0 THEN '$' + SentenceB END " +
                                     " AS[Best Scenario], CASE IsYr  WHEN 1 THEN SentenceGot + ' years' WHEN 0 THEN '$' + SentenceGot END  AS Result, '$' + CONVERT(varchar, Fee) AS Fee " +
                                     " FROM Journal INNER JOIN Clients ON Journal.ClD = Clients.CID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID" +
                                      " INNER JOIN Attorneys ON Attorneys.AttID = Journal.AttID WHERE SentenceGot > SentenceB ORDER BY Journal.CaseID;";
                        break;
                    }
            }
            if (selected != -1)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(que, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                dT = new DataTable();
                adapter.Fill(dT);
                MainGrid.ItemsSource = dT.DefaultView;
                connection.Close();
            }
            else
                System.Windows.MessageBox.Show("Оберіть таблицю перед друком!");
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dT != null)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.FileName = "Table";
                sf.Filter = "CSV file, (*.csv) |*.csv";
                DialogResult result = sf.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    var lines = new List<string>();

                    string[] columnNames = dT.Columns
                        .Cast<DataColumn>()
                        .Select(column => column.ColumnName)
                        .ToArray();

                    var header = string.Join("|", columnNames.Select(name => $"{name}"));
                    lines.Add(header);

                    var valueLines = dT.AsEnumerable()
                        .Select(row => string.Join("|", row.ItemArray.Select(val => $"{val}")));

                    lines.AddRange(valueLines);

                    File.WriteAllLines(sf.FileName + ".csv", lines);
                    System.Windows.MessageBox.Show("Таблицю роздруковано!");
                }
            }
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            FuncW wn = new FuncW();
            wn.Show();
            Close();
        }
    }
}
