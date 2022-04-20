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
using System.IO;




namespace Prj_Soft_Protection
{
    /// <summary>
    /// Interaction logic for StudyModeWindow.xaml
    /// </summary>
    /// 



    public partial class StudyModeWindow : Window
    {
        static int TriesNum = 1, KeysCount = 0, TriesTotal = 0;
        static string CodeText = "";
        static Stopwatch stopWatch = new Stopwatch();
        static double[,] Intervals;
        static double[,] MCD;

        public StudyModeWindow()
        {
            InitializeComponent();

        }

        private void ErrorMsg()
        {
            KeysCount = 0;
            InputField.Text = "";
            MessageBox.Show("Something went wrong...", "Error", MessageBoxButton.OK);
            stopWatch.Stop();
        }

        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (TriesTotal == 0 || CodeText.Length == 0)
            { ErrorMsg(); e.Handled = true; }
            ////
            else if (e.Key.ToString().ToLower() == CodeText[KeysCount].ToString())
            {
                KeysCount++;
                SymbolCount.Content = KeysCount.ToString();
                if (KeysCount == 1)
                {
                    stopWatch.Restart();
                }
                else if (KeysCount == CodeText.Length)
                {
                    Intervals[TriesNum - 1, KeysCount - 1] = stopWatch.ElapsedMilliseconds / 1000.0;
                    stopWatch.Stop();
                    int check = IntervalCheck(TriesNum - 1);
                    if (check == 1)
                    {
                        MessageBox.Show("Successfull try" + "\nKeys: " + KeysCount + "\nTries: " + TriesNum, "Success", MessageBoxButton.OK);
                        InputField.Text = "";
                        e.Handled = true;
                        DataCalc(TriesNum - 1);
                        KeysCount = 0;

                        TriesNum++;
                    }
                    else
                    {
                        MessageBox.Show("Not a successfull try, Tp > Tp from the table (alpha = 0.05)" + "\nKeys: " + KeysCount + "\nTries: " + TriesNum, "Success", MessageBoxButton.OK);
                        InputField.Text = "";
                        e.Handled = true;
                        KeysCount = 0;
                    }
                    if (TriesNum == TriesTotal)
                    {
                        stopWatch.Stop();
                        MessageBox.Show("Tries кончились");
                        e.Handled = true;
                        InputField.Text = "";
                        InputField.IsEnabled = false;
                    }
                }
                else
                {
                    Intervals[TriesNum - 1, KeysCount - 1] = stopWatch.ElapsedMilliseconds / 1000.0;
                    stopWatch.Restart();
                }
            }
            ////
            else
            { ErrorMsg(); e.Handled = true; }
        }

        private void CountProtection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                TriesTotal = int.Parse(e.Key.ToString().Substring(1, 1));
                Intervals = new double[TriesTotal, CodeText.Length];
                MCD = new double[2, TriesTotal];
            }
            else
                ErrorMsg();
        }

        private void Verified_KeyDown(object sender, KeyEventArgs e)
        {
            CodeText += e.Key.ToString().ToLower();
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {

            using (StreamWriter stream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/IntervalsTime.txt"))
            {
                stream.WriteLine(CodeText);
                for (int i = 0; i < MCD.GetLength(1); i++)
                { 
                    if (MCD[0, i] != 0 && MCD[1, i] != 0)
                       stream.WriteLine(MCD[0, i] + "/" + MCD[1, i]); 
                }
            }
            KeysCount = 0; TriesNum = 0; InputField.Text = ""; stopWatch.Stop();
            MainWindow nwc = new MainWindow();
            Hide();
            nwc.Show();
        }

        private void DataCalc(int tries)
        {
            double MC = 0, Dtry = 0;
            for (int j = 1; j < Intervals.GetLength(1); j++)
                MC += Intervals[tries, j];
            MC /= Intervals.GetLength(1);
            MCD[0, tries] = MC;
            for (int i = 1; i < Intervals.GetLength(1); i++)
                Dtry += (Intervals[tries, i] - MC) * (Intervals[tries, i] - MC);
            Dtry /= Intervals.GetLength(1) - 1;
            MCD[1, tries] = Dtry;
        }

        private int IntervalCheck(int tries)
        {
            int check = 1; double M = 0, D = 0, Dsqrt = 0, Tp;
            for (int j = 1; j < Intervals.GetLength(1); j++)
            {
                double y = Intervals[tries, j];
                for (int k = 1; k < Intervals.GetLength(1); k++)
                {
                    if (k == j)
                        continue;
                    else
                        M += Intervals[tries, k];
                }
                M /= Intervals.GetLength(1) - 1;
                D = getD(y, tries, M);
                Dsqrt = Math.Sqrt(D);
                Tp = Math.Abs((y - M) / Dsqrt);
                if (Tp > 2.364) //df = 7, alpha = 0.05
                { check *= 0; }
            }
            return check;
        }
        private double getD(double y, int j, double M)
        {
            double D = 0;
            for (int i = 0; i < Intervals.GetLength(1); i++)
            {
                if (Intervals[j, i] == y)
                    continue;
                else
                    D += (Intervals[j, i] - M) * (Intervals[j, i] - M);
            }
            D /= Intervals.GetLength(1) - 2;
            return D;
        }
    }
}
