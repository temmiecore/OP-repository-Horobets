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
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Prj_Soft_Protection
{
    /// <summary>
    /// Interaction logic for ProtectionModeWindow.xaml
    /// </summary>
    public partial class ProtectionModeWindow : Window
    {
        static int TriesNum = 1, KeysCount = 0, TriesTotal = 0;
        static string CodeText = "";
        static Stopwatch stopWatch = new Stopwatch();
        static double[,] Intervals;
        static double Err1 = 1, Err2 = 1;

        public ProtectionModeWindow()
        {
            InitializeComponent();
            VerifField.Text = File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/IntervalsTime.txt").First();
            CodeText = VerifField.Text;
        }
        private void ErrorMsg()
        {
            KeysCount = 0;
            InputField.Text = "";
            MessageBox.Show("Something went wrong...", "Error", MessageBoxButton.OK);
            stopWatch.Stop();
        }

        private void Triestextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                TriesTotal = int.Parse(e.Key.ToString().Substring(1, 1));
                Intervals = new double[TriesTotal, CodeText.Length];
            }
            else
                ErrorMsg();
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            KeysCount = 0; TriesNum = 0; InputField.Text = "";
            MainWindow nwc = new MainWindow();
            Hide();
            nwc.Show();
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
                        MessageBox.Show("Successfull try" + "\nTries: " + TriesNum, "Success", MessageBoxButton.OK);
                        AuthenticationMethod(TriesNum - 1);
                        InputField.Text = "";
                        e.Handled = true;
                        KeysCount = 0;
                        TriesNum++;
                    }
                    else
                    {
                        MessageBox.Show("Not a successfull try, Tp > T", "Success", MessageBoxButton.OK);
                        InputField.Text = "";
                        e.Handled = true;
                        KeysCount = 0;
                    }
                    if (TriesNum == TriesTotal)
                    {
                        stopWatch.Stop();
                        MessageBox.Show("No tries left");
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

        private void AuthenticationMethod(int tries)
        {
            double Err1tries = 0, Err2tries = 0;
            double MC = 0, Dtry = 0;
            for (int j = 1; j < Intervals.GetLength(1); j++)
                MC += Intervals[tries, j];
            MC /= Intervals.GetLength(1);
            MessageBox.Show(MC.ToString());
            for (int i = 1; i < Intervals.GetLength(1); i++)
                Dtry += (Intervals[tries, i] - MC) * (Intervals[tries, i] - MC);
            Dtry /= Intervals.GetLength(1) - 1;

            double r = 0, k = 0;
            foreach (string line in File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/IntervalsTime.txt").Skip(1))
            {
                double MCref = double.Parse(line.Split("/")[0]), Dref = double.Parse(line.Split("/")[1]);
                double Fp = Math.Max(MC, MCref) / Math.Min(MC, MCref);
                if (Fp > 3.79) //7 letters, alpha = 0.05
                    DispField.Content = "неоднорідні";
                else
                    DispField.Content = "однорідні";

                double S = (Math.Sqrt((Dtry + Dref) * (Intervals.GetLength(1) - 1)) / (2 * Intervals.GetLength(1) - 1));
                double Tp = Math.Abs(MC - MCref) / (S * Math.Sqrt(2.0 / Intervals.GetLength(1)));
                if (Tp <= 2.45) //n-1 = 6, 0.05
                {
                    r++;
                    if (Legituser.IsChecked == false)
                        Err2tries++;
                }
                else
                {
                    if (Legituser.IsChecked == true)
                        Err1tries++;
                }
                k++;
                MessageBox.Show(r + "/" + k);
            }
            if (Legituser.IsChecked == true && Err1 != 0)
                Err1 *= (Err1tries / k);
            else if (Err1 == 0)
                Err1 = (Err1tries / k);
            if (Legituser.IsChecked == false && Err2 != 0) 
                Err2 *= (Err2tries / k);
            else if (Err2 == 0)
                Err2 = (Err2tries / k);
            double P = r / k;
            StatisticsBlock.Content = P.ToString();
            P1Field.Content = Err1.ToString();
            P2Field.Content = Err2.ToString();
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
