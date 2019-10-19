namespace MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class DocuaUserSettingItems
    {
        [XmlElement(Order = 0, ElementName = "DocuaUserSettingItem")]
        public List<DocuaUserSetting> UserSettingItems { get; set; }
    }
}