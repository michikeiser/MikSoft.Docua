namespace MikSoft.Docua.Common.Services.GlobalSettings
{
    public interface IGlobalSettingsRepository
    {
        string Get(string key);

        void AddAndSave(string key, string value);
    }
}