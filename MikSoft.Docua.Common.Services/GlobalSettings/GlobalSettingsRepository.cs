namespace MikSoft.Docua.Common.Services.GlobalSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings;
    using MikSoft.Docua.Common.Services.UserSettings;

    public class GlobalSettingsRepository : IGlobalSettingsRepository
    {
        private readonly IGlobalSettingsPersistenceService _persistenceService;

        private readonly IUserSettingsRepository _userSettingsRepository;

        public GlobalSettingsRepository(IGlobalSettingsPersistenceService persistenceService, IUserSettingsRepository userSettingsRepository)
        {
            _persistenceService = persistenceService;
            _userSettingsRepository = userSettingsRepository;
        }

        public string Get(string key)
        {
            var settingsEntries = LoadSettingsEntries();
            var settingsEntry = settingsEntries.SingleOrDefault(x => x.Key == key);
            return settingsEntry?.Value;
        }

        public void AddAndSave(string key, string value)
        {
            var settingsEntries = LoadSettingsEntries();
            var settingsEntry = settingsEntries.SingleOrDefault(x => x.Key == key);

            if (settingsEntry == null)
            {
                settingsEntries.Add(
                    new GlobalSettingsEntry
                        {
                            Key = key,
                            Value = value
                        });
            }
            else
            {
                settingsEntry.Value = value;
            }

            var repositoryPath = _userSettingsRepository.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH);
            _persistenceService.Save(repositoryPath, settingsEntries);
        }

        private List<GlobalSettingsEntry> LoadSettingsEntries()
        {
            var repositoryPath = _userSettingsRepository.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH);
            var settingsEntries = _persistenceService.Load(repositoryPath).ToList();
            return settingsEntries;
        }
    }
}