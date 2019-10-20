namespace MikSoft.Docua.Common.Services.UserSettings
{
    using System.Linq;

    using MikSoft.Docua.Common.Data.FileSystem.UserSettings;

    public class UserSettingsRepository : IUserSettingsRepository
    {
        private readonly IUserSettingsPersistenceService _persistenceService;

        public UserSettingsRepository(IUserSettingsPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        public string Get(string key)
        {
            var settingsEntries = _persistenceService.Load();
            var settingsEntry = settingsEntries.SingleOrDefault(x => x.Key == key);
            return settingsEntry?.Value;
        }

        public void AddAndSave(string key, string value)
        {
            var settingsEntries = _persistenceService.Load().ToList();
            var settingsEntry = settingsEntries.SingleOrDefault(x => x.Key == key);

            if (settingsEntry == null)
            {
                settingsEntries.Add(
                    new UserSettingsEntry
                        {
                            Key = key,
                            Value = value
                        });
            }
            else
            {
                settingsEntry.Value = value;
            }

            _persistenceService.Save(settingsEntries);
        }
    }
}