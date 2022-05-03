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
    /// Interaction logic for AddAttW.xaml
    /// </summary>
    public partial class AddAttW : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public AddAttW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ShowGrid();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] info = new string[5];
            if (NameSurname.Text != "" && Bdate.Text != "" && Adress.Text != "" && Phonenum.Text != "" && IDUpdate.Text == "")
            {
               
                info[0] = NameSurname.Text; info[1] = Bdate.Text; info[2] = Phonenum.Text; info[3] = Adress.Text; info[4] = Edu.Text;
                InsertData(info);
            }
            else if (IDUpdate.Text != "")
            {
                info[0] = NameSurname.Text; info[1] = Bdate.Text; info[2] = Phonenum.Text; info[3] = Adress.Text; info[4] = Edu.Text;
                InsertData(info);
            }
            else
                MessageBox.Show("Input all non optional data!");
        }

        private void InsertData(string[] info)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            string lastID = "";
            adapter = new SqlDataAdapter("SELECT TOP(100) PERCENT AttID AS ID, NameSurname AS Alias, PhoneNum AS[Phone Number], Adress, CASE WHEN Education IS NULL THEN 'No education / NI' ELSE Education END, BDate AS Birthday " +
                    "FROM     dbo.Attorneys " +
                    "ORDER BY ID ", connection);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            if (Table.Rows.Count > 0)
                lastID = (1 + Convert.ToInt32(Table.Rows[Table.Rows.Count - 1][0])).ToString();
            else
                lastID = "1";

            string que = "";
            string[] columns = new string[5] { "NameSurname", "BDate", "Adress", "PhoneNum", "Education" };
            if (IDUpdate.Text != "")
            {
                que += "UPDATE Attorneys SET ";
                for (int i = 0; i < info.Length; i++)
                    if (info[i] != "")
                        que += " " + columns[i] + " = '" + info[i] + "',";
                
                que = que.Remove(que.Length - 1);
                que += " WHERE ATTId = " + IDUpdate.Text + ";";
                MessageBox.Show(que);
            }
            else
            {
                if (info[4] != "")
                    que = "INSERT INTO Attorneys (ATTId, NameSurname, BDate, Adress, PhoneNum, Education)" +
                        " VALUES (" + lastID + ", '" + info[0] + "', '" + info[1] + "', '" + info[2] + "', '" + info[3] + "', '" + info[4] + "')";
                else
                    que = "INSERT INTO Attorneys (ATTId, NameSurname, BDate, Adress, PhoneNum)" +
                        " VALUES (" + lastID + ", '" + info[0] + "', '" + info[1] + "', '" + info[2] + "', '" + info[3] + "')";
            }
            command = new SqlCommand(que, connection);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
            connection.Close();
            NameSurname.Text = ""; Bdate.Text = ""; Adress.Text = ""; Phonenum.Text = ""; IDUpdate.Text = ""; Edu.Text = "";
            ShowGrid();

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
        private void ShowGrid()
        {
            string que = "SELECT TOP(100) PERCENT AttID AS ID, NameSurname AS Alias, PhoneNum AS[Phone Number], Adress, CASE WHEN Education IS NULL THEN 'No education / NI' ELSE Education END AS Education, BDate AS Birthday " +
                        "FROM     dbo.Attorneys " +
                        "ORDER BY ID ";
            GetAndDhowData(que, AttGrid);
        }

        private void RemBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID;
            if (RemoveID.Text != "")
            {
                ID = RemoveID.Text;
                string que = "DELETE FROM Attorneys WHERE AttID = " + ID + ";";
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand(que, connection);
                MessageBox.Show(command.ExecuteNonQuery().ToString());
                connection.Close();
                ShowGrid();
                RemoveID.Text = "";
            }
            else
                MessageBox.Show("Insert ID!");
        }
    }
}
