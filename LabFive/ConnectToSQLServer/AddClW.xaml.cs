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
    /// Interaction logic for AddClW.xaml
    /// </summary>
    public partial class AddClW : Window
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public AddClW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ShowGrid();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            byte phys;
            if (IsPhys.IsChecked == true)
                phys = 1;
            else
                phys = 0;
            if (phys == 0 && Bdate.Text != "")
                MessageBox.Show("Juridical clients can't have birthday date!");
            else if (Alias.Text != "" && Phonenum.Text != "" && Adress.Text != "")
                InsertData(Alias.Text, Bdate.Text, Phonenum.Text, Adress.Text, phys);
        }

        private void InsertData(string name, string bdate, string phone, string adress, byte isphys)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            string lastID = "";
            adapter = new SqlDataAdapter("SELECT TOP (100) PERCENT CID AS ID, Alias, Adress, "
                    + "CASE WHEN BDate IS NULL THEN 'Juridical cl.' ELSE CONVERT( varchar,BDate) END AS[Birthday Date], PhoneNum AS[Phone Number] "
                    + "FROM dbo.ClienTs ORDER BY ID", connection);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            if (Table.Rows.Count > 0)
                lastID = (1 + Convert.ToInt32(Table.Rows[Table.Rows.Count - 1][0])).ToString();
            else
                lastID = "1";

            string que;
            if (bdate != "")
                que = "INSERT INTO Clients (CID, IsPhys, Alias,  Adress, BDate, PhoneNum)" +
                    " VALUES (" + lastID + ", '"+ isphys +"', '" + name + "', '"+adress+  "', '" +  bdate+  "', '" + phone+"')";
            else
                que = "INSERT INTO Clients (CID, IsPhys, Alias,  Adress, PhoneNum)" +
                    " VALUES (" + lastID + ", '" + isphys + "', '" + name + "', '" + adress + "', '" + phone + "')";
            command = new SqlCommand(que, connection);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
            connection.Close();
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
            string que = "SELECT TOP (100) PERCENT CID AS ID, Alias, Adress, "
                    + "CASE WHEN BDate IS NULL THEN 'Juridical cl.' ELSE CONVERT( varchar,BDate) END AS[Birthday Date], PhoneNum AS[Phone Number] "
                    + "FROM dbo.ClienTs ORDER BY ID";
            GetAndDhowData(que, ClientGrid);
        }

        private void RemBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID;
            if (RemoveID.Text != "")
            {
                ID = RemoveID.Text;
                string que = "DELETE FROM Clients WHERE CID = " + ID + ";";
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
