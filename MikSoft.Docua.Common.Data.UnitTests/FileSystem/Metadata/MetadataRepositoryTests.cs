namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.Metadata
{
    using System;
    using System.IO;

    using AutoFixture.NUnit3;

    using FakeItEasy;

    using MikSoft.Docua.Common.Data.FileSystem.Common;
    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.Data.FileSystem.Inbox;
    using MikSoft.Docua.Common.Data.FileSystem.Metadata;
    using MikSoft.Docua.Common.Data.FileSystem.Metadata.Xml;
    using MikSoft.Docua.Common.TestWrappers;

    using NUnit.Framework;

    [TestFixture]
    internal class MetadataRepositoryTests
    {
        [Test, AutoData]
        public void Add_NewMetadataEntry_CallWriteAllText(MetadataEntry metadataEntry, string serializedString)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();
            A.CallTo(() => xmlSerializerHelper.Serialize(A<Metadata>.Ignored)).Returns(serializedString);

            var sut = new MetadataRepository(xmlSerializerHelper);

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            sut.FileTestWrapper = fileTestWrapper;

            // act
            sut.Add(metadataEntry);

            // assert
            A.CallTo(() => fileTestWrapper.WriteAllText(metadataEntry.FileName, serializedString)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Add_NewMetadataEntry_CallSerializer(MetadataEntry metadataEntry, string serializedString)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();
            A.CallTo(() => xmlSerializerHelper.Serialize(A<Metadata>.Ignored)).Returns(serializedString);

            var sut = new MetadataRepository(xmlSerializerHelper);

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            sut.FileTestWrapper = fileTestWrapper;

            // act
            sut.Add(metadataEntry);

            // assert
            A.CallTo(() => xmlSerializerHelper.Serialize(A<Metadata>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Add_NoFileName_ThrowException(string serializedString)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();
            A.CallTo(() => xmlSerializerHelper.Serialize(A<Metadata>.Ignored)).Returns(serializedString);

            var sut = new MetadataRepository(xmlSerializerHelper);

            // act
            var testDelegate = new TestDelegate(() => sut.Add(new MetadataEntry()));

            // assert
            Assert.That(testDelegate, Throws.ArgumentException);
        }

        [Test, AutoData]
        public void AddFile_Default_CallCreateEntry(string path, string searchPattern, SearchOption searchOption, string[] files)
        {
            // arrange
            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            A.CallTo(() => directoryTestWrapper.GetFiles(path, searchPattern, searchOption)).Returns(files);

            var eventHandler = A.Fake<EventHandler<RepositoryChangedEventArgs<MetadataEntry>>>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();

            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var sut = new MetadataRepository(xmlSerializerHelper)
            {
                DirectoryTestWrapper = directoryTestWrapper,
                FileTestWrapper = fileTestWrapper
            };

            sut.RepositoryChanged += eventHandler;

            // act
            sut.StartMonitoring(path, searchPattern, searchOption);
            sut.StopMonitoring();

            // assert
            // Check if event is raised
            A.CallTo(
                () => eventHandler.Invoke(
                    A<object>._,
                    A<RepositoryChangedEventArgs<MetadataEntry>>.That.Matches(
                        x => x.Action == RepositoryChangedEventArgs<MetadataEntry>.RepositoryAction.Add))).MustHaveHappened();
        }

    }
}