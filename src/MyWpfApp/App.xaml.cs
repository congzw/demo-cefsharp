using System;
using System.Windows;
using MyCommon;

namespace MyWpfApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var environmentHelper = EnvironmentHelper.Instance;
            var environmentInfo = environmentHelper.CreateEnvironmentInfo();
            Console.WriteLine(environmentInfo.Is64BitProcess);
            Console.WriteLine(environmentHelper.ArchSpecificPath());

            //MessageBox.Show(environmentInfo.ToIniString(null));
        }
    }
}
