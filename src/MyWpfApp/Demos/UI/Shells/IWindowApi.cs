namespace MyWpfApp.Demos.UI.Shells
{
    public interface IWindowApi
    {
        void ShowWindowMessage(WindowMessage msg);

        void SwitchPosition();

        //void OpenWindow(object args);
        //void CloseWindow(object args);
        //void SetWindow(object args);
        //void CaptureWindow(object args);
    }

    public interface IWindowCommandArgs
    {
        string WindowId { get; set; }
    }

    public class ShellConst
    {
        public static string WindowId_Shell1 = "Shell1";
        public static string WindowId_Shell2 = "Shell2";
        public static ShellConst Ext = new ShellConst();
    }

    public class WindowMessage : IWindowCommandArgs
    {
        public string WindowId { get; set; }
        public string Message { get; set; }
    }
}
