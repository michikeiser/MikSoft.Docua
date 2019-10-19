namespace MikSoft.Docua.Client.Services
{
    using MikSoft.Docua.Common.Interfaces;
    using MikSoft.Docua.Common.Services.UserSettings;

    internal class SettingsService
    {
        private readonly IUserSettingsRepository _userSettingsRepository;

        public SettingsService(IUserSettingsRepository userSettingsRepository)
        {
            _userSettingsRepository = userSettingsRepository;
        }

        public string RepositoryPath
        {
            get => _userSettingsRepository.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH);
            set => _userSettingsRepository.AddAndSave(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH, value);
        }
    }
}