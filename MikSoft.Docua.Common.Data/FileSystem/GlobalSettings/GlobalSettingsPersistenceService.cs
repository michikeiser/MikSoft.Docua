namespace MikSoft.Docua.Common.Data.FileSystem.GlobalSettings
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings.XmlNodes;
    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.TestWrappers;

    internal class GlobalSettingsPersistenceService : IGlobalSettingsPersistenceService
    {
        private const string GLOBAL_SETTINGS_FILE = "GlobalSettings.xml";

        private readonly GlobalSettingConverter _globalSettingConverter;

        private readonly XmlSerializerHelper _xmlSerializerHelper;

        public GlobalSettingsPersistenceService(XmlSerializerHelper xmlSerializerHelper, GlobalSettingConverter globalSettingConverter)
        {
            _xmlSerializerHelper = xmlSerializerHelper;
            _globalSettingConverter = globalSettingConverter;

            FileTestWrapper = new FileTestWrapper();
            PathTestWrapper = new PathTestWrapper();
            DirectoryTestWrapper = new DirectoryTestWrapper();
        }

        internal FileTestWrapper FileTestWrapper { get; set; }

        internal PathTestWrapper PathTestWrapper { get; set; }

        internal DirectoryTestWrapper DirectoryTestWrapper { get; set; }

        public IEnumerable<GlobalSettingsEntry> Load(string path)
        {
            var globalSettingsPath = GetGlobalSettingsPath(path);

            if (FileTestWrapper.Exists(globalSettingsPath))
            {
                var content = FileTestWrapper.ReadAllText(globalSettingsPath);
                var globalSettingItems = _xmlSerializerHelper.GetObject<DocuaGlobalSettingItems>(content);
                var globalSettingsEntries = _globalSettingConverter.ToSettingsEntries(globalSettingItems.GlobalSettingItems);
                return globalSettingsEntries;
            }

            return new List<GlobalSettingsEntry>();
        }

        public void Save(string path, IEnumerable<GlobalSettingsEntry> payload)
        {
            var globalSettingsPath = GetGlobalSettingsPath(path);

            if (!DirectoryTestWrapper.Exists(globalSettingsPath))
            {
                DirectoryTestWrapper.CreateDirectory(globalSettingsPath);
            }

            var docuaGlobalSettings = _globalSettingConverter.ToDocuaSettings(payload).ToList();
            var docuaGlobalSettingItems = new DocuaGlobalSettingItems
                                              {
                                                  GlobalSettingItems = docuaGlobalSettings
                                              };

            var str = _xmlSerializerHelper.GetString(docuaGlobalSettingItems);

            FileTestWrapper.WriteAllText(globalSettingsPath, str);
        }

        private string GetGlobalSettingsPath(string path)
        {
            var globalSettingsPath = PathTestWrapper.Combine(path, GLOBAL_SETTINGS_FILE);
            return globalSettingsPath;
        }
    }
}