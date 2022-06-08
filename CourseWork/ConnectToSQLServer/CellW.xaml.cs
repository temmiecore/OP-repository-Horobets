using System;
using System.Windows;
using System.Windows.Media;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConnectToSQLServer
{
    public partial class CellW : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        public CellW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID1 = IDBox1.Text, ID2 = IDBox2.Text;
            if (ID1 != "" && ID2 != "")
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (Convert.ToInt32(new SqlCommand("SELECT IsPhys FROM ClienTs WHERE CID = " + ID1, connection).ExecuteScalar()) == 1
                    && Convert.ToInt32(new SqlCommand("SELECT IsPhys FROM ClienTs WHERE CID = " + ID2, connection).ExecuteScalar()) == 1)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT PrisonName FROM Journal INNER JOIN ClienTs ON Journal.ClD = ClienTs.CID WHERE (IsOver = 1 AND (CID = " + ID1+ " OR CID = " + ID2+"))", connection);
                    DataTable dT = new DataTable();
                    adapter.Fill(dT);
                    if (dT.Rows.Count == 2)
                    {
                        if (dT.Rows[0][0] != DBNull.Value && dT.Rows[1][0] != DBNull.Value)
                        {
                            string prison1 = Convert.ToString(dT.Rows[0][0]);
                            string prison2 = Convert.ToString(dT.Rows[1][0]);
                            if (prison1 == prison2)
                            {
                                adapter = new SqlDataAdapter("SELECT CellNum FROM Journal INNER JOIN ClienTs ON Journal.ClD = ClienTs.CID WHERE (IsOver = 1 AND (ClienTs.CID = " + ID1 + " OR ClienTs.CID = " + ID2+"))", connection);
                                dT = new DataTable();
                                adapter.Fill(dT);
                                if (dT.Rows[0][0] != DBNull.Value && dT.Rows[1][0] != DBNull.Value)
                                {
                                    int cell1 = Convert.ToInt32(dT.Rows[0][0]);
                                    int cell2 = Convert.ToInt32(dT.Rows[1][0]);
                                    if (cell1 == cell2)
                                    {
                                        CheckLabel.Content = "Камери співпадають!";
                                        CheckLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6B0000"));
                                    }
                                    else
                                    {
                                        CheckLabel.Content = "Камери не співпадають!";
                                        CheckLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF006B05"));
                                    }
                                    connection.Close();
                                }
                                else
                                    CheckLabel.Content = "Один з клієнтів не знаходиться у камері.";
                            }
                            else
                                CheckLabel.Content = "Клієнти знаходятся в різних тюрмах.";
                        }
                        else
                            CheckLabel.Content = "Один з клієнтів не знаходится у тюрьмі.";
                    }
                    else
                        CheckLabel.Content = "Клієнта ще не додано до журналу," + Environment.NewLine + " або його не існує.";
                }
                else
                    CheckLabel.Content = "Один з клієнтів - юр. особа.";
            }
            else
                MessageBox.Show("Заповніть обидва поля з ID!");
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            FuncW wn = new FuncW();
            wn.Show();
            Close();
        }
    }
}
