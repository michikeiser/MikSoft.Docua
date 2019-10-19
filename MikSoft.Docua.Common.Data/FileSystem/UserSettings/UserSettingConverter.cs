namespace MikSoft.Docua.Common.Data.FileSystem.UserSettings
{
    using System.Collections.Generic;

    using MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes;
    using MikSoft.Docua.Common.Models;

    internal class UserSettingConverter
    {
        public IEnumerable<SettingsEntry> ToSettingsEntries(IEnumerable<DocuaUserSetting> userSettings)
        {
            foreach (var userSetting in userSettings)
            {
                yield return new SettingsEntry
                                 {
                                     Key = userSetting.Key,
                                     Value = userSetting.Value
                                 };
            }
        }

        public IEnumerable<DocuaUserSetting> ToDocuaUserSettings(IEnumerable<SettingsEntry> settingsEntries)
        {
            foreach (var settingsEntry in settingsEntries)
            {
                yield return new DocuaUserSetting
                                 {
                                     Key = settingsEntry.Key,
                                     Value = settingsEntry.Value
                                 };
            }
        }
    }
}