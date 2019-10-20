namespace MikSoft.Docua.Common.Data.FileSystem.GlobalSettings
{
    using System.Collections.Generic;

    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings.XmlNodes;

    internal class GlobalSettingConverter
    {
        public virtual IEnumerable<GlobalSettingsEntry> ToSettingsEntries(IEnumerable<DocuaGlobalSetting> userSettings)
        {
            foreach (var userSetting in userSettings)
            {
                yield return new GlobalSettingsEntry
                                 {
                                     Key = userSetting.Key,
                                     Value = userSetting.Value
                                 };
            }
        }

        public virtual IEnumerable<DocuaGlobalSetting> ToDocuaSettings(IEnumerable<GlobalSettingsEntry> settingsEntries)
        {
            foreach (var settingsEntry in settingsEntries)
            {
                yield return new DocuaGlobalSetting
                                 {
                                     Key = settingsEntry.Key,
                                     Value = settingsEntry.Value
                                 };
            }
        }
    }
}