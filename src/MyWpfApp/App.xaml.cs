using System.Windows;
using MyWpfApp.Demos.Boots;

namespace MyWpfApp
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            EntryLogic.Startup(this);
        }
    }
}
