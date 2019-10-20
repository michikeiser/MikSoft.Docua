namespace MikSoft.Docua.Common.Data.FileSystem.UserSettings
{
    using System.Collections.Generic;

    using MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes;

    internal class UserSettingConverter
    {
        public virtual IEnumerable<UserSettingsEntry> ToSettingsEntries(IEnumerable<DocuaUserSetting> userSettings)
        {
            foreach (var userSetting in userSettings)
            {
                yield return new UserSettingsEntry
                                 {
                                     Key = userSetting.Key,
                                     Value = userSetting.Value
                                 };
            }
        }

        public virtual IEnumerable<DocuaUserSetting> ToDocuaSettings(IEnumerable<UserSettingsEntry> settingsEntries)
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