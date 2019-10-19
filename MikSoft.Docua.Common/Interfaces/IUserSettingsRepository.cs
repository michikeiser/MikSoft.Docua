namespace MikSoft.Docua.Common.Interfaces
{
    public interface IUserSettingsRepository
    {
        string Get(string key);

        void AddAndSave(string key, string value);
    }
}