using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;




namespace Prj_Soft_Protection
{
    /// <summary>
    /// Interaction logic for StudyModeWindow.xaml
    /// </summary>
    public partial class StudyModeWindow : Window
    {
        static int TriesNum = 0, KeysCount = 0;
        static string ControlText = "";
        static Stopwatch stopWatch = new Stopwatch();
        static List<long> Intervals = new List<long>();
        public StudyModeWindow()
        {
            InitializeComponent();
            ControlText = VerifField.Text;
        }
        private void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (CountProtection.SelectedItem != null)
                MessageBox.Show("Please choose tries count.", "ComboxError", MessageBoxButton.OK);
            else
            {
                KeysCount++;
                
                if (KeysCount == 1)
                    stopWatch.Start();
                else
                {
                    long bufftime = stopWatch.ElapsedMilliseconds;
                    Intervals.Add(bufftime);
                    if ((InputField.Text.ToString())[KeysCount-1] != (ControlText.ToString())[KeysCount-1])
                    {
                        Intervals.Clear();
                        KeysCount = 0;
                        InputField.Text = "";
                        MessageBox.Show("Input error, try isn't counted", "Input error", MessageBoxButton.OK);
                        stopWatch.Stop();
                    }
                }
            }
        }

        private void MainMethod(int Tries)
        {
            Intervals.Clear(); KeysCount = 0; TriesNum = 0; InputField.Text = "";
            stopWatch.Stop();
            while (TriesNum < Tries)
            {
                if (KeysCount == (ControlText.ToString()).Length)
                {
                    stopWatch.Stop();
                    //Мат. часть будет тут...
                    MessageBox.Show("Successfull try, gratz", "Success", MessageBoxButton.OK);
                    Intervals.Clear();
                    KeysCount = 0;
                    TriesNum++;
                }
            }
            //Вторая мат. часть тут...
            //И запись в файл
            MessageBox.Show("Tries кончились, иди спать");
        }



        private void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountProtection.Text != "")
              MainMethod(int.Parse(CountProtection.Text));
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nwc = new MainWindow();
            Hide();
            nwc.Show();
        }
    }
}
