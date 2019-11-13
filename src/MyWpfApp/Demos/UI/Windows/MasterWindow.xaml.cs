using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MyWpfApp.Demos.UI.Shells;

namespace MyWpfApp.Demos.UI.Windows
{
    public partial class MasterWindow : Window
    {
        public bool AllowedClose { get; set; }

        public MasterWindow()
        {
            InitializeComponent();

            this.Hide();
            ShowShellWindows();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!AllowedClose)
            {
                e.Cancel = true;
                MessageBox.Show("Close Not Allowed!");
            }
            base.OnClosing(e);
        }
        
        public void ShowShellWindows()
        {
            //todo 根据配置和环境显示1~N个Shell窗体

            var shellWindow1 = new ShellWindow();
            shellWindow1.WindowId = ShellConst.WindowId_Shell1;
            shellWindow1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            shellWindow1.WindowState = WindowState.Maximized;
            shellWindow1.Show();

            var shellWindow2 = new ShellWindow();
            shellWindow2.WindowId = ShellConst.WindowId_Shell2;
            shellWindow2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            shellWindow2.WindowState = WindowState.Maximized;
            shellWindow2.Show();
            var shellApi = new ShellApi(this, shellWindow1, shellWindow2);

            //var shellApi = new ShellApi(this, shellWindow1);
            var ctrlWindow = new DemoControlWindow(shellApi);
            ctrlWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ctrlWindow.WindowState = WindowState.Normal;
            ctrlWindow.Show();
            ctrlWindow.Topmost = true;
        }
    }
}
