namespace MikSoft.Docua.Common.Services.UserSettings
{
    using System.Linq;

    using MikSoft.Docua.Common.Interfaces;
    using MikSoft.Docua.Common.Models;

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
                    new SettingsEntry
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