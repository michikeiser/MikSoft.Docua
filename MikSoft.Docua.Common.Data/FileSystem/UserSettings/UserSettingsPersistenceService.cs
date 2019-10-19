namespace MikSoft.Docua.Common.Data.FileSystem.UserSettings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes;
    using MikSoft.Docua.Common.Interfaces;
    using MikSoft.Docua.Common.Models;

    internal class UserSettingsPersistenceService : IUserSettingsPersistenceService
    {
        private const string PATH = "Docua";

        private const string USER_SETTING_FILE = "UserSettings.xml";

        private readonly UserSettingConverter _userSettingConverter;

        private readonly XmlSerializerHelper _xmlSerializerHelper;

        public UserSettingsPersistenceService(XmlSerializerHelper xmlSerializerHelper, UserSettingConverter userSettingConverter)
        {
            _xmlSerializerHelper = xmlSerializerHelper;
            _userSettingConverter = userSettingConverter;
        }

        public IEnumerable<SettingsEntry> Load()
        {
            var userSettingsPath = GetUserSettingsPath();
            var userSettingsFilePath = GetUserSettingsFilePath(userSettingsPath);

            if (File.Exists(userSettingsFilePath))
            {
                var content = File.ReadAllText(userSettingsFilePath);
                var userSettings = _xmlSerializerHelper.GetObject<DocuaUserSettingItems>(content);

                var settingsEntries = _userSettingConverter.ToSettingsEntries(userSettings.UserSettingItems).ToList();
                return settingsEntries;
            }

            return new List<SettingsEntry>();
        }

        public void Save(IEnumerable<SettingsEntry> payload)
        {
            var userSettingsPath = GetUserSettingsPath();

            if (!Directory.Exists(userSettingsPath))
            {
                Directory.CreateDirectory(userSettingsPath);
            }

            var docuaUserSettings = _userSettingConverter.ToDocuaUserSettings(payload).ToList();
            var docuaUserSettingItems = new DocuaUserSettingItems
                                            {
                                                UserSettingItems = docuaUserSettings
                                            };

            var str = _xmlSerializerHelper.GetString(docuaUserSettingItems);

            var userSettingsFilePath = GetUserSettingsFilePath(userSettingsPath);
            File.WriteAllText(userSettingsFilePath, str);
        }

        private string GetUserSettingsPath()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userSettingsPath = Path.Combine(appDataPath, PATH);

            return userSettingsPath;
        }

        private string GetUserSettingsFilePath(string userSettingsPath)
        {
            var path = Path.Combine(userSettingsPath, USER_SETTING_FILE);
            return path;
        }
    }
}