namespace MikSoft.Docua.Common.Data.FileSystem.UserSettings
{
    using System.Collections.Generic;

    public interface IUserSettingsPersistenceService
    {
        IEnumerable<UserSettingsEntry> Load();

        void Save(IEnumerable<UserSettingsEntry> payload);
    }
}