namespace MikSoft.Docua.Common.Data
{
    using System.Diagnostics.CodeAnalysis;

    using DryIoc;

    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings;
    using MikSoft.Docua.Common.Data.FileSystem.UserSettings;
    using MikSoft.Docua.Common.Interfaces;

    [ExcludeFromCodeCoverage]
    public class CommonDataTypeRegistration : IDryIocRegistration
    {
        public void Load(IRegistrator registrator)
        {
            registrator.Register<IUserSettingsPersistenceService, UserSettingsPersistenceService>();
            registrator.Register<IGlobalSettingsPersistenceService, GlobalSettingsPersistenceService>();
        }
    }
}