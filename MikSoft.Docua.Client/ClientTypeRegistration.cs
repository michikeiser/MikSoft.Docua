namespace MikSoft.Docua.Client
{
    using DryIoc;

    using MikSoft.Docua.Client.Services;
    using MikSoft.Docua.Client.ViewModels;
    using MikSoft.Docua.Common.Interfaces;

    public class ClientTypeRegistration : IDryIocRegistration
    {
        public void Load(IRegistrator registrator)
        {
            registrator.Register<ShellView>();
            registrator.Register<MainViewModel>();
            registrator.Register<SettingsService>();
        }
    }
}