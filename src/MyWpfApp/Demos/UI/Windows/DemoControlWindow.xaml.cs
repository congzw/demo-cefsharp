using System.ComponentModel;
using System.Windows;
using MyWpfApp.Demos.UI.Shells;

namespace MyWpfApp.Demos.UI.Windows
{
    public partial class DemoControlWindow : Window
    {
        public ShellApiContext ShellApiContext { get; }

        public DemoControlWindow(ShellApiContext shellApiContext)
        {
            ShellApiContext = shellApiContext;
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var shellApi1 = ShellApiContext.ShellApi1;
            shellApi1.CloseApp();
            shellApi1.ShowTask(true);
            base.OnClosing(e);
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            var shellApi = ShellApiContext.ShellApi1;
            shellApi.Shutdown();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            var shellApi = ShellApiContext.ShellApi2 ?? ShellApiContext.ShellApi1;
            shellApi.Restart();
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            var shellApi = ShellApiContext.ShellApi1;
            shellApi.CloseApp();
        }

        private void RestartAppButton_Click(object sender, RoutedEventArgs e)
        {
            var shellApi = ShellApiContext.ShellApi1;
            shellApi.RestartApp();
        }

        private bool showTask = true;
        private void SwitchTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var shellApi = ShellApiContext.ShellApi1;
            showTask = !showTask;
            shellApi.ShowTask(showTask);
        }

        private void SwitchPositionButton_Click(object sender, RoutedEventArgs e)
        {
            var shellApi = ShellApiContext.ShellApi1;
            shellApi.SwitchPosition();
        }
    }
}
