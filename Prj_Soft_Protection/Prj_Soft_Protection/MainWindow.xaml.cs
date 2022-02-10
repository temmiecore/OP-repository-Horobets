using System.Windows;
namespace Prj_Soft_Protection
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void StudyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            StudyModeWindow studyModeWindow = new StudyModeWindow();
            studyModeWindow.Show();
            Hide();
        }
        private void ProtectionModeBtn_Click(object sender, RoutedEventArgs e)

        {
            ProtectionModeWindow protectionModeWindow = new ProtectionModeWindow();
            protectionModeWindow.Show();
            Hide();
        }
    }
}
