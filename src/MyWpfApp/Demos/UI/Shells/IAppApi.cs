namespace MyWpfApp.Demos.UI.Shells
{
    public interface IAppApi
    {
        //关闭应用，调试用
        void CloseApp();
        //重启应用
        void RestartApp();
        //升级应用
        void UpdateApp();
        //获取应用环境信息
        AppEnvInfo GetAppEnvInfo();
    }
    public class AppEnvInfo
    {
        //todo
    }
}