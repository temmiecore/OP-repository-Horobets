using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Linq;


namespace LabTwo._2
{
    class Window2
    {
        TextBox read1 = new TextBox();
        TextBox read2 = new TextBox();
        TextBox read3 = new TextBox();
        TextBox read4 = new TextBox();
        Window wn = new Window();

        public Window2()
        {
            initControls();
        }


        private void initControls()
        {
            wn.Title = "Data Reader"; wn.Height = 320; wn.Width = 470; wn.Background = Brushes.Lavender;
            Grid grid = new Grid();
            grid.Width = 460; grid.Height = 310;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            RowDefinition[] rows = new RowDefinition[13];
            ColumnDefinition[] cols = new ColumnDefinition[13];
            for (int i = 0; i < 13; i++)
            {
                rows[i] = new RowDefinition();
                grid.RowDefinitions.Add(rows[i]);
                cols[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(cols[i]);
            }

            //Labels
            Label studid = new Label(); studid.Content = "Student ID:"; studid.FontSize = 16; studid.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(studid, 1); Grid.SetColumn(studid, 1); Grid.SetColumnSpan(studid, 3);
            Label names = new Label(); names.Content = "1st and 2nd Names:"; names.FontSize = 16; names.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(names, 3); Grid.SetColumn(names, 1); Grid.SetColumnSpan(names, 5);
            Label fac = new Label(); fac.Content = "Facultee:"; fac.FontSize = 16; fac.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(fac, 5); Grid.SetColumn(fac, 1); Grid.SetColumnSpan(fac, 3);
            Label groupid = new Label(); groupid.Content = "Group ID:"; groupid.FontSize = 16; groupid.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(groupid, 7); Grid.SetColumn(groupid, 1); Grid.SetColumnSpan(groupid, 3);

            //Textboxes
            read1.FontSize = 17; read1.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(read1, 1); Grid.SetColumn(read1, 6); Grid.SetColumnSpan(read1, 5);
            read2.FontSize = 17; read2.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(read2, 3); Grid.SetColumn(read2, 6); Grid.SetColumnSpan(read2, 6);
            read3.FontSize = 17; read3.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(read3, 5); Grid.SetColumn(read3, 6); Grid.SetColumnSpan(read3, 5);
            read4.FontSize = 17; read4.FontFamily = new FontFamily("SimSun-ExtB");
            Grid.SetRow(read4, 7); Grid.SetColumn(read4, 6); Grid.SetColumnSpan(read4, 5);

            //Buttons
            Button addbtn = new Button(); addbtn.FontSize = 18; addbtn.FontFamily = new FontFamily("SimSun-ExtB"); addbtn.Content = "Add stud.";
            addbtn.Click += new RoutedEventHandler(AddReader_Click);
            Grid.SetRow(addbtn, 9); Grid.SetColumn(addbtn, 1); Grid.SetColumnSpan(addbtn, 3); Grid.SetRowSpan(addbtn, 2);
            Button removebtn = new Button(); removebtn.FontSize = 18; removebtn.FontFamily = new FontFamily("SimSun-ExtB"); removebtn.Content = "Remove stud.";
            removebtn.Click += new RoutedEventHandler(RemoveReader_Click);
            Grid.SetRow(removebtn, 9); Grid.SetColumn(removebtn, 5); Grid.SetColumnSpan(removebtn, 3); Grid.SetRowSpan(removebtn, 2);
            Button tomain = new Button(); tomain.FontSize = 19; tomain.FontFamily = new FontFamily("SimSun-ExtB"); tomain.Content = "To main";
            tomain.Click += new RoutedEventHandler(_2ToMain_Click);
            Grid.SetRow(tomain, 9); Grid.SetColumn(tomain, 9); Grid.SetColumnSpan(tomain, 3); Grid.SetRowSpan(tomain, 2);

            grid.Children.Add(fac); grid.Children.Add(names); grid.Children.Add(groupid); grid.Children.Add(studid);
            grid.Children.Add(read1); grid.Children.Add(read2); grid.Children.Add(read3); grid.Children.Add(read4);
            grid.Children.Add(addbtn); grid.Children.Add(removebtn); grid.Children.Add(tomain);


            wn.Content = grid;
            wn.Show();
        }

        private void _2ToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nwc = new MainWindow();
            wn.Hide();
            nwc.Show();
        }

        private void AddReader_Click(object sender, RoutedEventArgs e)
        {
            string Student = "";
            if (read1.Text != "" && read2.Text != "" && read3.Text != "" && read4.Text != "" && read1.Text.Length == 4)
            {
                Student = "ID: " + read1.Text + "; Name: " + read2.Text + "; Facultee: " + read3.Text + "; Group: " + read4.Text + ".";
                using (StreamWriter sw = File.AppendText("Students.txt"))
                    sw.WriteLine(Student);
                MessageBox.Show("New entry added!", "Operation succes", MessageBoxButton.OK);
                read1.Text = ""; read2.Text = ""; read3.Text = ""; read4.Text = "";
            }
            else
                MessageBox.Show("Input all correct data into specified boxes!", "Reader error", MessageBoxButton.OK, MessageBoxImage.Error);


        }

        private void RemoveReader_Click(object sender, RoutedEventArgs e)
        {
            if (read1.Text != "" && File.Exists("Students.txt") && read1.Text.Length == 4)
            {
                string[] oldtxt = File.ReadAllLines("Students.txt");
                var newtxt = oldtxt.Where(line => !line.Contains(read1.Text));
                File.WriteAllLines("Students.txt", newtxt);
                MessageBox.Show("Entry deleted!", "Operation succes", MessageBoxButton.OK);
                read1.Text = ""; read2.Text = ""; read3.Text = ""; read4.Text = "";
            }
            else
                MessageBox.Show("Make sure .txt file / entry exists and ID is written correctly!", "Reader error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}