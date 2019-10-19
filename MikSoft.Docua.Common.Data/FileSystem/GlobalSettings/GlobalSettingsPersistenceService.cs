namespace MikSoft.Docua.Common.Data.FileSystem.GlobalSettings
{
    using System;
    using System.Collections.Generic;

    using MikSoft.Docua.Common.Interfaces;
    using MikSoft.Docua.Common.Models;

    public class GlobalSettingsPersistenceService : IGlobalSettingsPersistenceService
    {
        public IEnumerable<SettingsEntry> Load(string path)
        {
            throw new NotImplementedException();
        }

        public void Save(string path, IEnumerable<SettingsEntry> payload)
        {
            throw new NotImplementedException();
        }
    }
}