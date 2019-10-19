namespace MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class DocuaUserSetting
    {
        [XmlElement(Order = 0)]
        public string Key { get; set; }

        [XmlElement(Order = 1)]
        public string Value { get; set; }
    }
}