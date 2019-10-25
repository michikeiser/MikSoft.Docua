namespace MikSoft.Docua.Common.Data.FileSystem.Helper
{
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    internal class XmlSerializerHelper
    {
        public virtual string Serialize<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new Utf8StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public virtual T Deserialize<T>(string str)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var textReader = new StringReader(str))
            {
                var obj = xmlSerializer.Deserialize(textReader);
                return (T)obj;
            }
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}