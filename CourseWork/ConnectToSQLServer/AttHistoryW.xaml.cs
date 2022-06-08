using System;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConnectToSQLServer
{
    public partial class AttHistoryW : Window
    {
        string que;

        string connectionString = null;
        SqlConnection connection = null;
        public AttHistoryW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void PickBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IDBox.Text != "" && int.TryParse(IDBox.Text, out int ID))
            {
                que = "SELECT Journal.CaseID AS [Case ID], ClienTs.Alias AS Client, Cases.CrimeType AS Crime, '$'+CONVERT(varchar,Cases.Fee) AS Fee, Journal.CaseStart AS [Starting Date], Journal.CaseClosed AS [Closing date]," +
                            "   CASE WHEN IsYr = 1 THEN Journal.SentenceGot + ' p.' ELSE '$' + Journal.SentenceGot END AS Result"+
                            "   FROM Attorneys INNER JOIN"+
                            "   Journal ON Attorneys.AttID = Journal.AttID INNER JOIN"+
                             " ClienTs ON Journal.ClD = ClienTs.CID INNER JOIN"+
                             "   Cases ON Journal.CaseID = Cases.CaseID"+
                              "    WHERE(Attorneys.AttID = "+ID+") AND IsOver = 1;";
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(que, connection);
                DataTable dT = new DataTable();
                adapter.Fill(dT);
                MainGrid.ItemsSource = dT.DefaultView;
                connection.Close();
            }
            else
                MessageBox.Show("Введіть правильне ID адвоката!");
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            FuncW wn = new FuncW();
            wn.Show();
            Close();
        }
    }
}
