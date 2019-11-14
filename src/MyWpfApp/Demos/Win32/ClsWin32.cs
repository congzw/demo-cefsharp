using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

namespace MyWpfApp.Demos.Win32
{
    public class ClsWin32
    {
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr handleParent, IntPtr handleChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        public static string Shell_TrayWnd = "Shell_TrayWnd";

        public static void ShowTask(bool show)
        {
            var trayHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, Shell_TrayWnd, null);
            ShowWindow(trayHandle, show ? (uint) 1 : (uint)0);
        }

        public static void SwitchFullScreen(Form theForm)
        {
            if (theForm.WindowState == FormWindowState.Maximized)
            {
                //theForm.FormBorderStyle = FormBorderStyle.None;
                theForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                theForm.WindowState = FormWindowState.Normal;
                ShowTask(true);
            }
            else
            {
                theForm.FormBorderStyle = FormBorderStyle.None;
                theForm.WindowState = FormWindowState.Maximized;
                ShowTask(false);
            }
        }
    }
}
