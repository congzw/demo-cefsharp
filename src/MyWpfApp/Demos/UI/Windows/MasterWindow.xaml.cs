using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MyWpfApp.Demos.UI.Shells;
using MyWpfApp.Demos.Win32;

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

        public ShellApi ShellApi { get; set; }

        public void ShowShellWindows()
        {
            //根据配置和环境显示1~2个Shell窗体
            var wpfScreens = WpfScreen.AllScreens().ToList();
            var wpfScreensCount = wpfScreens.Count;
            if (wpfScreensCount <= 1)
            {
                var shellWindow1 = CreateShellWindow();
                ShellApi = new ShellApi(this, shellWindow1);
            }
            else
            {
                var shellWindows = CreateShellWindows();
                ShellApi = new ShellApi(this, shellWindows.ToArray());
            }

            //是否隐藏任务条
            ShellApi.ShowTask = false;
            
            foreach (var window in ShellApi.ShellWindows)
            {
                window.WindowStyle = WindowStyle.None;
                window.ResizeMode = ResizeMode.NoResize;
                window.Show();
            }

            var ctrlWindow = new DemoControlWindow(ShellApi);
            ctrlWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ctrlWindow.WindowState = WindowState.Normal;
            ctrlWindow.Show();
            ctrlWindow.Topmost = true;
        }

        private ShellWindow CreateShellWindow()
        {
            var shellWindow1 = new ShellWindow();
            shellWindow1.WindowId = ShellConst.WindowId_Shell1;
            
            shellWindow1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            shellWindow1.WindowState = WindowState.Maximized;
            //shellWindow1.Title = WpfScreen.Primary.DeviceName;
            return shellWindow1;
        }
        private IList<ShellWindow> CreateShellWindows()
        {
            var primary = WpfScreen.Primary;
            
            var shellWindow1 = new ShellWindow();
            shellWindow1.WindowId = ShellConst.WindowId_Shell1;
            //shellWindow1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //shellWindow1.WindowState = WindowState.Maximized;

            shellWindow1.Left = primary.DeviceBounds.Left;
            shellWindow1.Top = primary.DeviceBounds.Top;
            shellWindow1.Width = primary.DeviceBounds.Width;
            shellWindow1.Height = primary.DeviceBounds.Height;
            shellWindow1.Title = primary.DeviceName;
            shellWindow1.Background = Brushes.White;

            var second = WpfScreen.AllScreens().Single(x => !x.IsPrimary);
            var shellWindow2 = new ShellWindow();
            shellWindow2.WindowId = ShellConst.WindowId_Shell2;
            //shellWindow2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //shellWindow2.WindowState = WindowState.Maximized;

            shellWindow2.Left = second.DeviceBounds.Left;
            shellWindow2.Top = second.DeviceBounds.Top;
            shellWindow2.Width = second.DeviceBounds.Width;
            shellWindow2.Height = second.DeviceBounds.Height;
            shellWindow2.Title = second.DeviceName;
            shellWindow1.Background = Brushes.Yellow;

            var shellWindows = new List<ShellWindow>();
            shellWindows.Add(shellWindow1);
            shellWindows.Add(shellWindow2);
            return shellWindows;
        }
    }
}
