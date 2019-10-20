namespace MikSoft.Docua.Common.Data.FileSystem.GlobalSettings
{
    using System.Collections.Generic;

    public interface IGlobalSettingsPersistenceService
    {
        IEnumerable<GlobalSettingsEntry> Load(string path);

        void Save(string path, IEnumerable<GlobalSettingsEntry> payload);
    }
}