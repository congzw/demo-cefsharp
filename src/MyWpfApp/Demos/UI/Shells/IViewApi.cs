namespace MyWpfApp.Demos.UI.Shells
{
    public interface IViewApi
    {
        void InvokeViewCommand(ViewCommand cmd);
    }
    public interface IViewCommandArgs
    {
        string View { get; set; }
    }

    public class ViewCommand : IViewCommandArgs
    {
        public string View { get; set; }
    }
}