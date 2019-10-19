namespace MikSoft.Docua.Common.Interfaces
{
    using System.Collections.Generic;

    using MikSoft.Docua.Common.Models;

    public interface IUserSettingsPersistenceService
    {
        IEnumerable<SettingsEntry> Load();

        void Save(IEnumerable<SettingsEntry> payload);
    }
}