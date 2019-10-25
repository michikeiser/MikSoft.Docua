namespace MikSoft.Docua.Common.Data.FileSystem.Metadata
{
    using System;

    using MikSoft.Docua.Common.Data.FileSystem.Common;

    public class MetadataEntry : IFileInfo
    {
        public DateTime AddedAt { get; set; }

        public string AddedFrom { get; set; }

        public string ContentFilename { get; set; }

        public DateTime Date { get; set; }

        public string DocuaVersion { get; set; }

        public string DocumentNumber { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string OcrFilename { get; set; }

        public PropertyValueEntry[] PropertyValues { get; set; }

        public string[] Tags { get; set; }

        public string FileName { get; set; }

        public class PropertyValueEntry
        {
            public string Id { get; set; }

            public string Value { get; set; }
        }
    }
}