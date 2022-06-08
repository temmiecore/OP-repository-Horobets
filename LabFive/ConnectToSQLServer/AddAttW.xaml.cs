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
        int bookmarkIndex = 1;
        public AddAttW()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ShowGrid();
        }


        //Add button
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] info = new string[8];
            switch (bookmarkIndex)
            {
                case 1:
                    {
                        if (Alias.Text != "" && Bdate.Text != "" && Adress.Text != "" && Phonenum.Text != "" && IDUpdate.Text == "")
                        {
                            info[0] = Alias.Text; info[1] = Bdate.Text; info[3] = Phonenum.Text; info[2] = Adress.Text; info[4] = EduAt.Text;
                            InsertData(info);
                        }
                        else if (IDUpdate.Text != "")
                        {
                            info[0] = Alias.Text; info[1] = Bdate.Text; info[2] = Phonenum.Text; info[3] = Adress.Text; info[4] = EduAt.Text;
                            InsertData(info);
                        }
                        else
                            MessageBox.Show("Input all non optional data!");
                        break;
                    }
                case 2:
                    {
                        byte phys;
                        if (IsPhys.IsChecked == true)
                            phys = 1;
                        else phys = 0;
                        if (phys == 0 && Bdate.Text != "")
                            MessageBox.Show("Juridical clients can't have birthday date!");
                        else if (Alias.Text != "" && Phonenum.Text != "" && Adress.Text != "" && IDUpdate.Text == "")
                        {
                            info[1] = Alias.Text; info[3] = Bdate.Text; info[4] = Phonenum.Text; info[2] = Adress.Text; info[0] = phys.ToString();
                            InsertData(info);
                        }
                        else if (IDUpdate.Text != "")
                        {
                            info[1] = Alias.Text; info[3] = Bdate.Text; info[4] = Phonenum.Text; info[2] = Adress.Text; info[0] = phys.ToString();
                            InsertData(info);
                        }
                        else
                            MessageBox.Show("Input all data!");
                        break;
                    }
                case 3:
                    {
                        byte year;
                        if (IsYr.IsChecked == true)
                            year = 1;
                        else year = 0;
                        if (SentenceB.Text != "" && SentenceW.Text != "" && Crimetype.Text != "" && Fee.Text != "" && IDUpdate.Text == "")
                        {
                            info[0] = Crimetype.Text; info[1] = year.ToString(); info[2] = SentenceB.Text; info[3] = SentenceW.Text; info[4] = Fee.Text;
                            InsertData(info);
                        }
                        else if (IDUpdate.Text != "")
                        {
                            info[0] = Crimetype.Text; info[1] = year.ToString(); info[2] = SentenceB.Text; info[3] = SentenceW.Text; info[4] = Fee.Text;
                            InsertData(info);
                        }
                        else
                            MessageBox.Show("Input all data!");
                        break;
                    }
                case 4:
                    {
                        byte over;
                        if (IsOver.IsChecked == true)
                            over = 1;
                        else over = 0; 
                        if (IDatt.Text != "" && IDcl.Text != "" && Datestart.Text != "" && IDUpdate.Text != "")
                        {
                            info[0] = IDatt.Text; info[1] = IDcl.Text; info[2] = Datestart.Text; info[3] = over.ToString();
                            info[4] = Result.Text; info[5] = Dateover.Text; info[6] = Prison.Text; info[7] = Cellnum.Text; 
                            InsertData(info);
                        }
                        else if (IDUpdate.Text == "")
                        {
                            info[0] = IDatt.Text; info[1] = IDcl.Text; info[2] = Datestart.Text; info[3] = over.ToString();
                            info[4] = Result.Text; info[5] = Dateover.Text; info[6] = Prison.Text; info[7] = Cellnum.Text;
                            InsertData(info);
                        }
                        else
                            MessageBox.Show("Input all data!");
                        break;
                    }
            }
        }

        //Insert/Update
        private void InsertData(string[] info)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string que = "";
            string lastID = "";


            switch (bookmarkIndex)
            {
                case 1:
                    {
                        adapter = new SqlDataAdapter("SELECT * FROM Attorneys ORDER BY ATTId", connection);
                        DataTable Table = new DataTable();
                        adapter.Fill(Table);
                        if (Table.Rows.Count > 0)
                            lastID = (1 + Convert.ToInt32(Table.Rows[Table.Rows.Count - 1][0])).ToString();
                        else
                            lastID = "1";
                        string[] columns = new string[5] { "NameSurname", "BDate", "Adress", "PhoneNum", "Education" };
                        if (IDUpdate.Text != "")
                        {
                            que += "UPDATE Attorneys SET ";
                            for (int i = 0; i < columns.Length; i++)
                                if (info[i] != "")
                                    que += " " + columns[i] + " = '" + info[i] + "',";
                            que = que.Remove(que.Length - 1);
                            que += " WHERE ATTId = " + IDUpdate.Text + ";";
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
                        command.ExecuteNonQuery();
                        MessageBox.Show("Attorney added/updated succesfully!");
                        break;
                    }
                case 2:
                    {
                        adapter = new SqlDataAdapter("SELECT * FROM Clients ORDER BY CID", connection);
                        DataTable Table = new DataTable();
                        adapter.Fill(Table);
                        if (Table.Rows.Count > 0)
                            lastID = (1 + Convert.ToInt32(Table.Rows[Table.Rows.Count - 1][0])).ToString();
                        else
                            lastID = "1";
                        string[] columns = new string[5] { "IsPhys", "Alias", "Adress", "BDate", "PhoneNum" };
                        if (IDUpdate.Text != "")
                        {
                            que += "UPDATE Clients SET ";
                            for (int i = 1; i < columns.Length; i++)
                                if (info[i] != "")
                                    que += " " + columns[i] + " = '" + info[i] + "',";

                            que = que.Remove(que.Length - 1);
                            que += " WHERE CID = " + IDUpdate.Text + ";";
                            MessageBox.Show(que);
                        }
                        else
                        {
                            if (info[1] != "")
                                que = "INSERT INTO Clients (CID, IsPhys, Alias,  Adress, BDate, PhoneNum)" +
                                    " VALUES (" + lastID + ", '" + info[0] + "', '" + info[1] + "', '" + info[2] + "', '" + info[3] + "', '" + info[4] + "')";
                            else
                                que = "INSERT INTO Clients (CID, IsPhys, Alias,  Adress, PhoneNum)" +
                                    " VALUES (" + lastID + ", '" + info[0] + "', '" + info[1] + "', '" + info[2] + "', '" + info[4] + "')";
                        }
                        command = new SqlCommand(que, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Client added succesfully!");
                        break;
                    }
                case 3:
                    {
                        adapter = new SqlDataAdapter("SELECT * FROM Cases ORDER BY CaseID", connection);
                        DataTable Table = new DataTable();
                        adapter.Fill(Table);
                        if (Table.Rows.Count > 0)
                            lastID = (1 + Convert.ToInt32(Table.Rows[Table.Rows.Count - 1][0])).ToString();
                        else
                            lastID = "1";
                        string[] columns = new string[5] { "CrimeType", "IsYr", "SentenceB", "SentenceW", "Fee" };
                        try
                        {
                            if (IDUpdate.Text != "")
                            {
                                que += "UPDATE Cases SET ";
                                for (int i = 0; i < columns.Length; i++)
                                    if (info[i] != "")
                                        que += " " + columns[i] + " = '" + info[i] + "',";

                                que = que.Remove(que.Length - 1);
                                que += " WHERE Cases.CaseID = " + IDUpdate.Text + ";";
                                MessageBox.Show(que);
                            }
                            else
                            {
                                if (info[1] != "")
                                    que = "INSERT INTO Cases (Cases.CaseID, CrimeType, IsYR,  SentenceB, SentenceW, Fee)" +
                                        " VALUES (" + lastID + ", '" + info[0] + "', '" + info[1] + "', '" + info[2] + "', '" + info[3] + "', '" + info[4] + "')";
                            }
                            command = new SqlCommand(que, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Case added succesfully!");
                        }
                        catch 
                        {
                            MessageBox.Show("Спочатку додайте справу до журналу!");
                        }
                        break;
                    }
                case 4:
                    {
                        adapter = new SqlDataAdapter("SELECT * FROM Journal ORDER BY CaseID", connection);
                        DataTable Table = new DataTable();
                        adapter.Fill(Table);
                        if (Table.Rows.Count > 0)
                            lastID = (1 + Convert.ToInt32(Table.Rows[Table.Rows.Count - 1][0])).ToString();
                        else
                            lastID = "1";
                        string[] columns = new string[8] {"Journal.AttID", "Journal.ClD", "CaseStart", "IsOver","SentenceGot","CaseClosed","PrisonName","CellNum"};
                        if (JournalUpd.Text != "")
                        {
                            que += "UPDATE Journal SET";
                            for (int i = 0; i < columns.Length; i++)
                                if (info[i] != "")
                                    que += " " + columns[i] + " = '" + info[i] + "',";

                            que = que.Remove(que.Length - 1);
                            que += " WHERE CaseID = " + IDUpdate.Text + ";";
                            MessageBox.Show(que);
                        }
                        else
                        {
                            que += "INSERT INTO Journal (CaseID, ";
                            for (int i = 0; i < columns.Length; i++)
                                if (info[i] != "")
                                    que += " " + columns[i] + ",";
                            que = que.Remove(que.Length - 1);
                            que += ") VALUES ("+lastID+", ";
                            for (int i = 0; i < columns.Length; i++)
                                if (info[i] != "")
                                    que += " '" + info[i] + "',";
                            que = que.Remove(que.Length - 1);
                            que += ");";
                            MessageBox.Show(que);
                        }
                        command = new SqlCommand(que, connection);
                        MessageBox.Show(command.ExecuteNonQuery().ToString());
                        MessageBox.Show("Journal added succesfully!");
                        break;
                    }
            }
            connection.Close();
            Alias.Text = ""; Bdate.Text = ""; Adress.Text = ""; Phonenum.Text = ""; IDUpdate.Text = ""; EduAt.Text = "";
            ShowGrid();
        }

        //Removal
        private void RemBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID;
            if (RemoveID.Text != "")
            {
                ID = RemoveID.Text;
                connection = new SqlConnection(connectionString);
                connection.Open();
                switch (bookmarkIndex)
                {
                    case 1:
                        {
                            string que = "DELETE FROM Attorneys WHERE AttID = " + ID + ";";
                            command = new SqlCommand(que, connection);
                            command.ExecuteNonQuery();
                            break;
                        }
                    case 2:
                        {
                            string que = "DELETE FROM Clients WHERE CID = " + ID + ";";
                            command = new SqlCommand(que, connection);
                            command.ExecuteNonQuery();
                            break;
                        }
                    case 3:
                        {
                            string que = "DELETE FROM Cases WHERE CaseID = " + ID + ";";
                            command = new SqlCommand(que, connection);
                            command.ExecuteNonQuery(); break;
                        }
                    case 4:
                        {
                            ID = JournalRm.Text;
                            string que = "DELETE FROM Journal WHERE CaseID = " + ID + ";";
                            command = new SqlCommand(que, connection);
                            command.ExecuteNonQuery(); break;
                        }
                }
                MessageBox.Show("Deletion succesful!");
                connection.Close();
                ShowGrid();
                RemoveID.Text = "";
            }
            else
                MessageBox.Show("Insert remove ID!");
        }



        //Bookmarks and helpers
        private void ShowGrid()
        {
            string que = "";
            switch (bookmarkIndex)
            {
                case 1:
                    {
                        que = "SELECT TOP(100) PERCENT ATTID AS ID, NameSurname AS Alias, PhoneNum AS[Phone Number], Adress, CASE WHEN Education IS NULL THEN 'No education / NI' ELSE Education END AS Education, BDate AS Birthday " +
                                 "FROM dbo.Attorneys " +
                                    "ORDER BY ID; ";
                        break;
                    }
                case 2:
                    {
                        que = "SELECT CID AS ID, Alias, Adress, PhoneNum AS [Phone Number], CASE WHEN IsPhys = 1 THEN CONVERT(varchar, BDate) ELSE 'Jur. Client' END AS [Birthday Date] " +
                                  " FROM dbo.ClienTs" +
                                      " ORDER BY ID; ";
                        break;
                    }
                case 3:
                    {
                        que = "SELECT CaseID AS ID, CrimeType AS [Тип порушення], CASE WHEN IsYr = 1 THEN 'Срок у тюрьмі' ELSE 'Грошовий штраф' END AS [Тип вироку], CASE WHEN IsYr = 1 THEN SentenceB +  ' р.' ELSE '$'+SentenceB END AS [Кращий випадок], CASE WHEN IsYr = 1 THEN SentenceW + ' р.' ELSE '$'+SentenceW END AS [Гірший випадок], '$'+CONVERT(varchar, Fee) AS Ціна " +
                                " FROM dbo.Cases " +
                                    " ORDER BY ID;";
                        break;
                    }
                case 4:
                    {
                        que = "SELECT TOP (100) PERCENT Journal.CaseID AS [Номер справи],'#'+ CONVERT(varchar, Attorneys.AttID) + ', ' + Attorneys.NameSurname AS Адвокат, '#'+ CONVERT(varchar,Clients.CID) + ', ' + ClienTs.Alias AS Клієнт, Journal.CaseStart AS [Дата початку], CASE WHEN IsOver = 1 THEN 'Закінчена' ELSE 'Не закінчена' END AS [Справа закінчена?],   ISNULL(CASE WHEN IsYr = 1 THEN SentenceGot + ' p.' ELSE '$' + SentenceGot END, 'NI')AS  [Результат - вирок]" +
                              " , ISNULL(CONVERT(varchar,CaseClosed), 'NI') AS [Дата закінчення], ISNULL(CASE WHEN IsYr = 0 THEN 'Вирок - штраф' ELSE PrisonName END, 'NI') AS Тюрьма, ISNULL(CASE WHEN IsYr = 0 THEN 'Вирок - штраф' ELSE '№ '+CONVERT(varchar, CellNum) END, 'NI') AS [Номер камери]" +
                                " FROM     Journal INNER JOIN Attorneys ON Journal.AttID = Attorneys.AttID INNER JOIN Cases ON Journal.CaseID = Cases.CaseID INNER JOIN ClienTs ON Journal.ClD = ClienTs.CID" +
                                     " ORDER BY Journal.CaseID;";
                        break;
                    }
            }
            GetAndDhowData(que, MainGrid);
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
        private void AttBookMark_Click(object sender, RoutedEventArgs e)
        {
            bookmarkIndex = 1;
            AttWindow.Visibility = Visibility.Visible; AttWindow.IsEnabled = true;
            CaseWindow.Visibility = Visibility.Hidden; CaseWindow.IsEnabled = false;
            ClientWindow.Visibility = Visibility.Hidden; ClientWindow.IsEnabled = false;
            ClAndAttWindow.Visibility = Visibility.Visible; ClAndAttWindow.IsEnabled = true;
            JournalWindow.Visibility = Visibility.Hidden; JournalWindow.IsEnabled = false;
            ShowGrid();

        }
        private void ClBookMark_Click(object sender, RoutedEventArgs e)
        {

            bookmarkIndex = 2;
            AttWindow.Visibility = Visibility.Hidden; AttWindow.IsEnabled = false;
            CaseWindow.Visibility = Visibility.Hidden; CaseWindow.IsEnabled = false;
            ClientWindow.Visibility = Visibility.Visible; ClientWindow.IsEnabled = true;
            ClAndAttWindow.Visibility = Visibility.Visible; ClAndAttWindow.IsEnabled = true;
            JournalWindow.Visibility = Visibility.Hidden; JournalWindow.IsEnabled = false;
            ShowGrid();
        }
        private void CaseBookMark_Click(object sender, RoutedEventArgs e)
        {

            bookmarkIndex = 3;
            AttWindow.Visibility = Visibility.Hidden; AttWindow.IsEnabled = false;
            ClAndAttWindow.Visibility = Visibility.Hidden; ClAndAttWindow.IsEnabled = false;
            CaseWindow.Visibility = Visibility.Visible; CaseWindow.IsEnabled = true;
            ClientWindow.Visibility = Visibility.Hidden; ClientWindow.IsEnabled = false;
            JournalWindow.Visibility = Visibility.Hidden; JournalWindow.IsEnabled = false;
            ShowGrid();
        }
        private void JournalBookMark_Click(object sender, RoutedEventArgs e)
        {
            bookmarkIndex = 4;
            JournalWindow.Visibility = Visibility.Visible; JournalWindow.IsEnabled = true;
            AttWindow.Visibility = Visibility.Hidden; AttWindow.IsEnabled = false;
            CaseWindow.Visibility = Visibility.Hidden; CaseWindow.IsEnabled = false;
            ClientWindow.Visibility = Visibility.Hidden; ClientWindow.IsEnabled = false;
            ClAndAttWindow.Visibility = Visibility.Hidden; ClAndAttWindow.IsEnabled = false;
            ShowGrid();
        }
    }
}
