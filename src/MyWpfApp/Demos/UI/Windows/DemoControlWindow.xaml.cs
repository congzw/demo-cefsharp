using System;
using System.ComponentModel;
using System.Windows;
using MyWpfApp.Demos.UI.Shells;

namespace MyWpfApp.Demos.UI.Windows
{
    public partial class DemoControlWindow : Window
    {
        public IShellApi ShellApi { get; }

        public DemoControlWindow(IShellApi shellApi)
        {
            ShellApi = shellApi;
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ShellApi.CloseApp();
            base.OnClosing(e);
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            ShellApi.Shutdown();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            ShellApi.Restart();
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            ShellApi.CloseApp();
        }

        private void RestartAppButton_Click(object sender, RoutedEventArgs e)
        {
            ShellApi.RestartApp();
        }
    }
}
