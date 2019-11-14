namespace MyWpfApp.Demos.UI.Shells
{
    public interface ISystemApi
    {
        //关机
        void Shutdown();
        //重启
        void Restart();
        //是否显示任务条
        bool ShowTask { get; set; }
    }
}