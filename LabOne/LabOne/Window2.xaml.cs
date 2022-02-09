using System.Windows;
using System.IO;
using System.Linq;

namespace LabOne
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void _2ToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nwc = new MainWindow();
            Hide();
            nwc.Show();
        }

        private void AddReader_Click(object sender, RoutedEventArgs e)
        {
            string Student = "";
            if (IDbox.Text != "" && Namebox.Text != "" && Facbox.Text != "" && Groupbox.Text != "" && IDbox.Text.Length == 4)
            {
                Student = "ID: " + IDbox.Text + "; Name: " + Namebox.Text + "; Facultee: " + Facbox.Text + "; Group: " + Groupbox.Text + ".";
                using (StreamWriter sw = File.AppendText("Students.txt"))
                    sw.WriteLine(Student);
                MessageBox.Show("New entry added!", "Operation succes", MessageBoxButton.OK);
                IDbox.Text = ""; Namebox.Text = ""; Facbox.Text = ""; Groupbox.Text = "";
            }
            else
                MessageBox.Show("Input all correct data into specified boxes!", "Reader error", MessageBoxButton.OK, MessageBoxImage.Error);
           
            
        }

        private void RemoveReader_Click(object sender, RoutedEventArgs e)
        {
            if (IDbox.Text != "" && File.Exists("Students.txt") && IDbox.Text.Length == 4)
            {
                string[] oldtxt = File.ReadAllLines("Students.txt");
                var newtxt = oldtxt.Where(line => !line.Contains(IDbox.Text));
                File.WriteAllLines("Students.txt", newtxt);
                MessageBox.Show("Entry deleted!", "Operation succes", MessageBoxButton.OK);
                IDbox.Text = ""; Namebox.Text = ""; Facbox.Text = ""; Groupbox.Text = "";
            }
            else
                MessageBox.Show("Make sure .txt file / entry exists and ID is written correctly!", "Reader error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
