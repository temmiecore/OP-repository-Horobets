using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Linq;


namespace LabTwo._2
{
    class Window5
    {
        TextBox read1 = new TextBox();
        TextBox read2 = new TextBox();
        TextBox read3 = new TextBox();
        TextBox read4 = new TextBox();
        Window wn = new Window();

        public Window5()
        {
            initControls();
        }


        private void initControls()
        {
            wn.Title = "About section"; wn.Height = 200; wn.Width = 300; wn.Background = Brushes.Lavender;
            Grid grid = new Grid();
            grid.Width = 300; grid.Height = 200;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            Label About = new Label(); About.FontSize = 20; About.FontFamily = new FontFamily("Times New Roman");
            About.Content = "About section\nГоробець Нікіта Олегович, 2022 р.н.\nГрупа КП-13, ФПМ, КПІ.";
            About.Margin = new Thickness(30, 20, 30, 100);
            grid.Children.Add(About);

            Button tomain = new Button(); tomain.FontSize = 24; tomain.FontFamily = new FontFamily("SimSun-ExtB"); tomain.Content = "Main menu";
            tomain.Margin = new Thickness(40, 120, 40, 30);
            tomain.Click += Button_Click;
            grid.Children.Add(tomain);


            wn.Content = grid;
            wn.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nwc = new MainWindow();
            wn.Hide();
            nwc.Show();
        }
    
    }
}