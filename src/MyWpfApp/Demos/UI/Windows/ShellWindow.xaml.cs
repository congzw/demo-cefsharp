using System.ComponentModel;
using System.Windows;

namespace MyWpfApp.Demos.UI.Windows
{
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

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
            MessageBox.Show(message);
            //todo
            //this.MessageBlock.Text = message;
        }
    }
}
