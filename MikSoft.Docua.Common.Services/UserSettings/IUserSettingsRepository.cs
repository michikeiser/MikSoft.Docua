namespace MikSoft.Docua.Common.Services.UserSettings
{
    public interface IUserSettingsRepository
    {
        string Get(string key);

        void AddAndSave(string key, string value);
    }
}