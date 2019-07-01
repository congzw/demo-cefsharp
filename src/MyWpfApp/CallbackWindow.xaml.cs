using System.Windows;
using MyCefLibs.CefBrowser;
using MyWpfApp.ViewModel;

namespace MyWpfApp
{
    /// <summary>
    /// CallbackWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CallbackWindow : Window
    {
        public CallbackWindowJs TheJs { get; set; }

        public CallbackWindow()
        {
            InitializeComponent();
            CustomizeInitializeComponent();
        }

        private void CustomizeInitializeComponent()
        {
            this.BtnLoad.Click += BtnLoad_Click;
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (TheJs != null)
            {
                return;
            }

            TheJs = new CallbackWindowJs();
            var demoPage = @"local://whatever/html/callback.html";
            var asyncJsObject = new AsyncJsObject();
            asyncJsObject.Name = "theVo";
            asyncJsObject.BindObject = this.TheJs;
            var helper = CefViewHelper.Create(asyncJsObject, demoPage);

            TheJs.MainCefViewHelper = helper;

            GridFrontPage.Children.Clear();
            helper.AppendCefBrowser(GridFrontPage);
        }
    }
}
