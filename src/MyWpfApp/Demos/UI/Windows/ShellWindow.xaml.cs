using System.ComponentModel;
using System.Windows;
using MyCefLibs.CefBrowser;
using MyWpfApp.Demos.UI.Shells;

namespace MyWpfApp.Demos.UI.Windows
{
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        public IShellApi ShellApi { get; set; }

        public void BindShellApi(ShellApi shellApi)
        {
            this.ShellApi = shellApi;
            shellApi.CurrentShell = this;
        }

        public void InitCefView(string entryUri)
        {
            var asyncJsObject = new AsyncJsObject();
            asyncJsObject.Name = "appHost";

            asyncJsObject.BindObject = ShellApi;

            CefView = CefViewHelper.Create(asyncJsObject, entryUri, args =>
            {
                var message = string.Format("{0} => {1}", args.Url, args.StatusCode);
                Dispatcher?.Invoke(() => { ShowMessage(message); });
            });

            CefView.AppendCefBrowser(GridForForeground);
        }

        public CefViewHelper CefView { get; set; }

        public string WindowId { get; set; }

        public bool AllowedClose { get; set; }

        protected override void OnClosing(CancelEventArgs e)
        {
            //can only be closed by:
            //1 master closed
            //2 application exit
            if (!AllowedClose)
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }

        public void ShowMessage(string message)
        {
            var displayMsg = string.Format("{0} => {1}", this.WindowId, message);
            this.MessageBlock.Text = displayMsg;
            Storyboard1.Begin();
        }
    }
}
