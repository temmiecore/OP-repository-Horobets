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
        static int TriesNum = 1, KeysCount = 0;

        static Stopwatch stopWatch = new Stopwatch();
        static List<long> Intervals = new List<long>();

        public StudyModeWindow()
        {
            InitializeComponent();

        }




        private void MainMethod(int Tries)
        {
            InputField.Text = ""; ;
            if (TriesNum != Tries)
            {
                stopWatch.Stop();
                //Мат. часть будет тут...
                double alpha = 0.05;
                int check = 1;
                for (int i = 0; i < Intervals.Count; i++)
                {
                    double y_i = Intervals[i];
                    double M_y = 0, M_y2 = 0, M2_y = 0, D = 0, Tp;
                    for (int j = 0; j < Intervals.Count; j++)
                    {
                        if (j == i)
                            continue;
                        else
                        {
                            M_y += Intervals[j];
                            M2_y += Intervals[j];
                            M_y2 += Math.Pow(Intervals[j], 2);
                        }
                        D = M_y2 - Math.Pow(M2_y, 2);
                        Tp = (y_i - M_y) / D;

                        if (Tp > 1.860)
                            check *= 0;
                    }
                }
                if (check == 0)
                {
                    MessageBox.Show("Not a successfull try due to user being dumb" + "\nTries: " + TriesNum, "Success", MessageBoxButton.OK);
                    Intervals.Clear();
                    KeysCount = 0;
                }
                else
                {
                    MessageBox.Show("Successfull try, gratz" + "\nKeys: " + KeysCount + "\nTries: " + TriesNum, "Success", MessageBoxButton.OK);
                    Intervals.Clear();
                    KeysCount = 0;
                    TriesNum++;
                }
                //А тут кончается
            }
            else
            {
                //Вторая мат. часть тут...
                //И запись в файл
                MessageBox.Show("Tries кончились, иди спать");
            }
        }



        private void ErrorMsg()
        {
            Intervals.Clear();
            KeysCount = 0;
            InputField.Text = "";
            MessageBox.Show("Something went terribly wrong i have no clue what it is", "Error", MessageBoxButton.OK);
            stopWatch.Stop();
        }

        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (CountProtection.SelectedItem == null)
            {
                InputField.Text = "";
                MessageBox.Show("Please choose tries count.\n" + CountProtection.Text, "ComboxError", MessageBoxButton.OK);
            }
            else
            {
                KeysCount++;
                MessageBox.Show(InputField.Text);
                SymbolCount.Content = KeysCount.ToString();
                if (KeysCount == 1)
                {
                    stopWatch.Start();
                    if (InputField.Text[KeysCount-1] != VerifField.Text[KeysCount-1] )
                        ErrorMsg();
                }
                else if (KeysCount == VerifField.Text.Length)
                {
                    MainMethod(int.Parse(CountProtection.Text));
                }
                else
                {
                    long bufftime = stopWatch.ElapsedMilliseconds;
                    Intervals.Add(bufftime);
                    if (InputField.Text[KeysCount] != VerifField.Text[KeysCount] )
                        ErrorMsg();
                }
            }
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            Intervals.Clear(); KeysCount = 0; TriesNum = 0; InputField.Text = "";
            stopWatch.Stop();
            MainWindow nwc = new MainWindow();
            Hide();
            nwc.Show();
        }
    }
}
