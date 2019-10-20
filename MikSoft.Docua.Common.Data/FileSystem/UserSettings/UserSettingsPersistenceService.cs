namespace MikSoft.Docua.Common.Data.FileSystem.UserSettings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes;
    using MikSoft.Docua.Common.TestWrappers;

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

            FileTestWrapper = new FileTestWrapper();
            DirectoryTestWrapper = new DirectoryTestWrapper();
            PathTestWrapper = new PathTestWrapper();
            EnvironmentTestWrapper = new EnvironmentTestWrapper();
        }

        internal FileTestWrapper FileTestWrapper { get; set; }

        internal DirectoryTestWrapper DirectoryTestWrapper { get; set; }

        internal PathTestWrapper PathTestWrapper { get; set; }

        internal EnvironmentTestWrapper EnvironmentTestWrapper { get; set; }

        public IEnumerable<UserSettingsEntry> Load()
        {
            var userSettingsPath = GetUserSettingsPath();
            var userSettingsFilePath = GetUserSettingsFilePath(userSettingsPath);

            if (FileTestWrapper.Exists(userSettingsFilePath))
            {
                var content = FileTestWrapper.ReadAllText(userSettingsFilePath);
                var userSettings = _xmlSerializerHelper.GetObject<DocuaUserSettingItems>(content);

                var settingsEntries = _userSettingConverter.ToSettingsEntries(userSettings.UserSettingItems).ToList();
                return settingsEntries;
            }

            return new List<UserSettingsEntry>();
        }

        public void Save(IEnumerable<UserSettingsEntry> payload)
        {
            var userSettingsPath = GetUserSettingsPath();

            if (!DirectoryTestWrapper.Exists(userSettingsPath))
            {
                DirectoryTestWrapper.CreateDirectory(userSettingsPath);
            }

            var docuaUserSettings = _userSettingConverter.ToDocuaSettings(payload).ToList();
            var docuaUserSettingItems = new DocuaUserSettingItems
                                            {
                                                UserSettingItems = docuaUserSettings
                                            };

            var str = _xmlSerializerHelper.GetString(docuaUserSettingItems);

            var userSettingsFilePath = GetUserSettingsFilePath(userSettingsPath);
            FileTestWrapper.WriteAllText(userSettingsFilePath, str);
        }

        private string GetUserSettingsPath()
        {
            var appDataPath = EnvironmentTestWrapper.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userSettingsPath = PathTestWrapper.Combine(appDataPath, PATH);

            return userSettingsPath;
        }

        private string GetUserSettingsFilePath(string userSettingsPath)
        {
            var path = PathTestWrapper.Combine(userSettingsPath, USER_SETTING_FILE);
            return path;
        }
    }
}