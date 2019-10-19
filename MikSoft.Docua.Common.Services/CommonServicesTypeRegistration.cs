namespace MikSoft.Docua.Common.Services
{
    using System.Diagnostics.CodeAnalysis;

    using DryIoc;

    using MikSoft.Docua.Common.Interfaces;
    using MikSoft.Docua.Common.Services.GlobalSettings;
    using MikSoft.Docua.Common.Services.UserSettings;

    [ExcludeFromCodeCoverage]
    public class CommonServicesTypeRegistration : IDryIocRegistration
    {
        public void Load(IRegistrator registrator)
        {
            registrator.Register<IUserSettingsRepository, UserSettingsRepository>();
            registrator.Register<IGlobalSettingsRepository, GlobalGlobalSettingsRepository>();
        }
    }
}