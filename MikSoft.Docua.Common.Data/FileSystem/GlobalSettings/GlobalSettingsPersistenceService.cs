namespace MikSoft.Docua.Common.Data.FileSystem.GlobalSettings
{
    using System;
    using System.Collections.Generic;

    public class GlobalSettingsPersistenceService : IGlobalSettingsPersistenceService
    {
        public IEnumerable<GlobalSettingsEntry> Load(string path)
        {
            throw new NotImplementedException();
        }

        public void Save(string path, IEnumerable<GlobalSettingsEntry> payload)
        {
            throw new NotImplementedException();
        }
    }
}