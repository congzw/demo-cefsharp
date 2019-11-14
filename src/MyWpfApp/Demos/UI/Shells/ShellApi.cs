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
        public MasterWindow Master { get; set; }
        public ShellWindow CurrentShell { get; set; }
        public IList<ShellWindow> AllShells { get; set; }

        public ShellApi(MasterWindow masterWindow, ShellWindow current, params ShellWindow[] shellWindows)
        {
            Master = masterWindow;
            AllShells = shellWindows.ToList();
            CurrentShell = current;
            current.ShellApi = this;
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

        public void ShowTask(bool show)
        {
            ClsWin32.ShowTask(show);
        }

        public void CloseApp()
        {
            Master.Dispatcher?.Invoke(() =>
            {
                Master.AllowedClose = true;
                Master.Close();
                //Application.Current.Shutdown(); // Environment.Exit(0); would also suffice 
            });
        }

        public void RestartApp()
        {
            Master.AllowedClose = true;
            Master.Dispatcher?.Invoke(RestartCurrentApp);
        }

        public void UpdateApp()
        {
            ShowWindowMessage(new WindowMessage() { Message = "TODO Updating..." });
        }

        public void ShowWindowMessage(WindowMessage model)
        {
            CurrentShell?.Dispatcher?.Invoke(() =>
            {
                CurrentShell.ShowMessage(model.Message);
            });
        }

        public void SwitchPosition()
        {
            CurrentShell?.Dispatcher?.Invoke(() =>
            {
                if (AllShells.Count <= 1)
                {
                    return;
                }

                var one = AllShells[0];
                var another = AllShells[1];
                ChangePosition(one, another);
            });
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

    public class ShellApiContext
    {
        public static ShellApiContext Current = new ShellApiContext();

        public ShellApi ShellApi1 { get; set; }
        public ShellApi ShellApi2 { get; set; }
    }
}