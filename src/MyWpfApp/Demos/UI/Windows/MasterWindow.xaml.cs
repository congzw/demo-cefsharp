using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MyWpfApp.Demos.Boots;
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

        public void ShowShellWindows()
        {
            //根据配置和环境显示1~2个Shell窗体
            var shellWindows = CreateShellWindows();

            var entrySetting = EntrySetting.Instance;
            var entryUri = entrySetting.StartupHtmlUri;

            foreach (var window in shellWindows)
            {
                window.WindowStyle = WindowStyle.None;
                window.ResizeMode = ResizeMode.NoResize;
                window.Show();
                window.InitCefView(entryUri);
            }

            var shellApiContext = ShellApiContext.Current;
            //todo by config: dev not hide!
            //shellApiContext.ShellApi1.ShowTask(true);

            var ctrlWindow = new DemoControlWindow(shellApiContext);
            ctrlWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ctrlWindow.WindowState = WindowState.Normal;
            ctrlWindow.Show();
            ctrlWindow.Topmost = true;
        }

        private IList<ShellWindow> CreateShellWindows()
        {
            var shellWindows = new List<ShellWindow>();
            var primary = WpfScreen.Primary;

            var shellWindow1 = new ShellWindow();
            shellWindow1.WindowId = ShellConst.WindowId_Shell1;

            shellWindow1.Left = primary.DeviceBounds.Left;
            shellWindow1.Top = primary.DeviceBounds.Top;
            shellWindow1.Width = primary.DeviceBounds.Width;
            shellWindow1.Height = primary.DeviceBounds.Height;
            shellWindow1.Title = primary.DeviceName;
            shellWindow1.Background = Brushes.YellowGreen;

            shellWindows.Add(shellWindow1);

            ShellWindow shellWindow2 = null;
            var second = WpfScreen.AllScreens().FirstOrDefault(x => !x.IsPrimary);
            if (second != null)
            {
                shellWindow2 = new ShellWindow();
                shellWindow2.WindowId = ShellConst.WindowId_Shell2;

                shellWindow2.Left = second.DeviceBounds.Left;
                shellWindow2.Top = second.DeviceBounds.Top;
                shellWindow2.Width = second.DeviceBounds.Width;
                shellWindow2.Height = second.DeviceBounds.Height;
                shellWindow2.Title = second.DeviceName;
                shellWindow2.Background = Brushes.DarkGray;

                shellWindows.Add(shellWindow2);
            }


            var shellApi1 = new ShellApi(this, shellWindow1, shellWindows.ToArray());
            shellWindow1.BindShellApi(shellApi1);
            ShellApiContext.Current.ShellApi1 = shellApi1;

            if (shellWindow2 != null)
            {
                var shellApi2 = new ShellApi(this, shellWindow2, shellWindows.ToArray());
                shellWindow2.BindShellApi(shellApi2);
                ShellApiContext.Current.ShellApi2 = shellApi2;
            }

            return shellWindows;
        }
    }
}
