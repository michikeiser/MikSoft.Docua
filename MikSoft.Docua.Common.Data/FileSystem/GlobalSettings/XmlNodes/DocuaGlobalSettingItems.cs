namespace MikSoft.Docua.Common.Data.FileSystem.GlobalSettings.XmlNodes
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class DocuaGlobalSettingItems
    {
        [XmlElement(Order = 0, ElementName = "DocuaUserSettingItem")]
        public List<DocuaGlobalSetting> GlobalSettingItems { get; set; }
    }
}