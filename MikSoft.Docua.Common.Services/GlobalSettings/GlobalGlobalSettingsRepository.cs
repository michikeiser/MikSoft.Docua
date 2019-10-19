namespace MikSoft.Docua.Common.Services.GlobalSettings
{
    using System;

    using MikSoft.Docua.Common.Interfaces;

    public class GlobalGlobalSettingsRepository : IGlobalSettingsRepository
    {
        private readonly IGlobalSettingsPersistenceService _globalSettingsPersistenceService;

        public GlobalGlobalSettingsRepository(IGlobalSettingsPersistenceService globalSettingsPersistenceService)
        {
            _globalSettingsPersistenceService = globalSettingsPersistenceService;
        }

        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public void AddAndSave(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}