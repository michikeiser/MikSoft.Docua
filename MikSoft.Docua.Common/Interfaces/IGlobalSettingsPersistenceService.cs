namespace MikSoft.Docua.Common.Interfaces
{
    using System.Collections.Generic;

    using MikSoft.Docua.Common.Models;

    public interface IGlobalSettingsPersistenceService
    {
        IEnumerable<SettingsEntry> Load(string path);

        void Save(string path, IEnumerable<SettingsEntry> payload);
    }
}