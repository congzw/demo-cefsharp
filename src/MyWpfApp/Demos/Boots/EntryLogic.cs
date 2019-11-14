using System;
using System.Windows;

namespace MyWpfApp.Demos.Boots
{
    public static class EntryLogic
    {
        public static void Startup(Application application)
        {
            //todo config
            var entrySetting = EntrySetting.Instance;
            entrySetting.StartupFormUri = "Demos/UI/Windows/MasterWindow.xaml";
            entrySetting.StartupHtmlUri = @"local://whatever/html/demo_control.html";

            application.StartupUri = new Uri(entrySetting.StartupFormUri, UriKind.Relative);
            application.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }

    public class EntrySetting
    {
        public string StartupFormUri { get; set; }
        public string StartupHtmlUri { get; set; }

        public static EntrySetting Instance = new EntrySetting();
    }
}
