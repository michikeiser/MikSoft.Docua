namespace MikSoft.Docua.Common.Interfaces
{
    public interface IGlobalSettingsRepository
    {
        string Get(string key);

        void AddAndSave(string key, string value);
    }
}