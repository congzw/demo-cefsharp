﻿using System;
using System.Windows;
using MyCefLibs.CefBrowser;

namespace MyWpfApp.Demos.Boots
{
    public static class EntryLogic
    {
        public static void Startup(Application application)
        {
            CefConfig.SupportAnyCpu();
            //todo config
            application.StartupUri = new Uri("Demos/UI/Windows/MasterWindow.xaml", UriKind.Relative);
            application.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //var environmentHelper = EnvironmentHelper.Instance;
            //var environmentInfo = environmentHelper.CreateEnvironmentInfo();
        }
    }
}