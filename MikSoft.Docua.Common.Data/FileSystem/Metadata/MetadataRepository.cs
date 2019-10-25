namespace MikSoft.Docua.Common.Data.FileSystem.Metadata
{
    using AutoMapper;

    using MikSoft.Docua.Common.Data.FileSystem.Common;
    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.Data.FileSystem.Metadata.Xml;

    internal class MetadataRepository : RepositoryBase<MetadataEntry>
    {
        private readonly IMapper _mapper;

        private readonly XmlSerializerHelper _xmlSerializerHelper;

        public MetadataRepository(XmlSerializerHelper xmlSerializerHelper)
        {
            _xmlSerializerHelper = xmlSerializerHelper;

            var config = new MapperConfiguration(
                cfg =>
                    {
                        cfg.CreateMap<Metadata, MetadataEntry>();
                        cfg.CreateMap<PropertyValue, MetadataEntry.PropertyValueEntry>();

                        cfg.CreateMap<MetadataEntry, Metadata>();
                        cfg.CreateMap<MetadataEntry.PropertyValueEntry, PropertyValue>();
                    });

            _mapper = config.CreateMapper();
        }

        protected override void SaveNewEntry(MetadataEntry entry)
        {
            var metadata = _mapper.Map<Metadata>(entry);

            var content = _xmlSerializerHelper.Serialize(metadata);
            FileTestWrapper.WriteAllText(entry.FileName, content);
        }

        protected override MetadataEntry CreateEntry(string content)
        {
            var metadata = _xmlSerializerHelper.Deserialize<Metadata>(content);

            var metadataEntry = _mapper.Map<MetadataEntry>(metadata);
            return metadataEntry;
        }
    }
}