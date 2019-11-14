using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MyWpfApp.Demos.UI.Windows;
using MyWpfApp.Demos.Win32;

namespace MyWpfApp.Demos.UI.Shells
{
    public interface IShellApi : ISystemApi, IAppApi, IWindowApi, IViewApi
    {
    }

    public class ShellApi : IShellApi
    {
        private bool _showTask;
        public MasterWindow MasterWindow { get; }

        public IList<ShellWindow> ShellWindows { get; set; }

        public ShellApi(MasterWindow masterWindow, params ShellWindow[] shellWindows)
        {
            MasterWindow = masterWindow;
            ShellWindows = shellWindows.ToList();
        }

        public void Shutdown()
        {
            ShowWindowMessage(new WindowMessage() { WindowId = ShellConst.WindowId_Shell1, Message = "TODO shutdown -s -t 0" });
            //try
            //{
            //    Process.Start(new ProcessStartInfo("shutdown", "-s -t 0")
            //    {
            //        UseShellExecute = false,
            //        CreateNoWindow = true
            //    });
            //}
            //catch (Exception ex)
            //{
            //    MyDebugHelper.Exception(ex);
            //}
        }

        public void Restart()
        {
            ShowWindowMessage(new WindowMessage() { WindowId = ShellConst.WindowId_Shell2, Message = "TODO shutdown -r -t 0" });
            //try
            //{
            //    Process.Start(new ProcessStartInfo("shutdown", "-r -t 0")
            //    {
            //        UseShellExecute = false,
            //        CreateNoWindow = true
            //    });
            //}
            //catch (Exception ex)
            //{
            //    MyDebugHelper.Exception(ex);
            //}
        }

        public bool ShowTask
        {
            get => _showTask;
            set
            {
                _showTask = value;
                ClsWin32.ShowTask(_showTask);
            }
        }

        public void CloseApp()
        {
            MasterWindow.Dispatcher?.Invoke(() =>
            {
                MasterWindow.AllowedClose = true;
                MasterWindow.Close();
                //Application.Current.Shutdown(); // Environment.Exit(0); would also suffice 
            });
        }

        public void RestartApp()
        {
            MasterWindow.AllowedClose = true;
            MasterWindow.Dispatcher?.Invoke(RestartCurrentApp);
        }

        public void UpdateApp()
        {
            ShowWindowMessage(new WindowMessage() { Message = "TODO Updating..." });
        }

        public void ShowWindowMessage(WindowMessage model)
        {
            var shellWindow = FindShellWindow(model, true);
            shellWindow?.Dispatcher?.Invoke(() => { shellWindow.ShowMessage(string.Format("{0} => ", shellWindow.WindowId) + model.Message); });
        }

        public void SwitchPosition()
        {
            if (ShellWindows.Count <= 1)
            {
                return;
            }

            var one = ShellWindows[0];
            var another = ShellWindows[1];
            ChangePosition(one, another);
        }

        public AppEnvInfo GetAppEnvInfo()
        {
            var appEnvInfo = new AppEnvInfo();
            return appEnvInfo;
        }

        public void InvokeViewCommand(ViewCommand cmd)
        {
            throw new System.NotImplementedException();
        }

        private void RestartCurrentApp()
        {
            Application.Current.Exit += (s, e) => { Process.Start(Application.ResourceAssembly.Location); };
            Application.Current.Shutdown();
        }

        private ShellWindow FindShellWindow(IWindowCommandArgs args, bool autoFindFirst = false)
        {
            var windowId = string.Empty;
            if (args != null)
            {
                if (!string.IsNullOrWhiteSpace(args.WindowId))
                {
                    windowId = args.WindowId.Trim();
                }
            }

            if (string.IsNullOrWhiteSpace(windowId))
            {
                if (autoFindFirst)
                {
                    windowId = ShellConst.WindowId_Shell1;
                }
            }

            var theOne =
                ShellWindows.FirstOrDefault(x => x.WindowId.Equals(windowId, StringComparison.OrdinalIgnoreCase));
            return theOne;
        }

        private void ChangePosition(ShellWindow one, ShellWindow another)
        {
            var left = one.Left;
            var top = one.Top;
            var width = one.Width;
            var height = one.Height;

            one.Left = another.Left;
            one.Top = another.Top;
            one.Width = another.Width;
            one.Height = another.Height;

            another.Left = left;
            another.Top = top;
            another.Width = width;
            another.Height = height;
        }
    }
}