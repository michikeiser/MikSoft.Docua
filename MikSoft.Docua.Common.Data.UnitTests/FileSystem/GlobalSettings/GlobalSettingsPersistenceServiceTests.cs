namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.GlobalSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.NUnit3;

    using FakeItEasy;

    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings;
    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings.XmlNodes;
    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.TestWrappers;

    using NUnit.Framework;

    [TestFixture]
    internal class GlobalSettingsPersistenceServiceTests
    {
        [Test, AutoData]
        public void Load_FileExisting_ReturnCallConverter(string path)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var globalSettingConverter = A.Fake<GlobalSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(true);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new GlobalSettingsPersistenceService(xmlSerializerHelper, globalSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load(path);

            // assert
            A.CallTo(() => globalSettingConverter.ToSettingsEntries(A<IEnumerable<DocuaGlobalSetting>>.That.Not.IsEmpty()))
                .MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Load_FileExisting_ReturnCallFileTestWrapper(string path)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var globalSettingConverter = A.Fake<GlobalSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(true);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new GlobalSettingsPersistenceService(xmlSerializerHelper, globalSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load(path);

            // assert
            A.CallTo(() => fileTestWrapper.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Load_FileExisting_ReturnCallSerializer(string path)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var globalSettingConverter = A.Fake<GlobalSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(true);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new GlobalSettingsPersistenceService(xmlSerializerHelper, globalSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load(path);

            // assert
            A.CallTo(() => xmlSerializerHelper.GetObject<DocuaGlobalSettingItems>(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Load_FileNotExists_ReturnEmptyList(string path)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var globalSettingConverter = A.Fake<GlobalSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(false);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new GlobalSettingsPersistenceService(xmlSerializerHelper, globalSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load(path);

            // assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test, AutoData]
        public void Save_DirectoryExists_CallConverter(string path, IEnumerable<GlobalSettingsEntry> settingsEntries)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var globalSettingConverter = A.Fake<GlobalSettingConverter>();

            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            var fileTestWrapper = A.Fake<FileTestWrapper>();
            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new GlobalSettingsPersistenceService(xmlSerializerHelper, globalSettingConverter)
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            sut.Save(path, settingsEntries);

            // assert
            A.CallTo(() => globalSettingConverter.ToDocuaSettings(A<IEnumerable<GlobalSettingsEntry>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Save_DirectoryNotExists_CallCreateDirectory(string path, IEnumerable<GlobalSettingsEntry> settingsEntries)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var globalSettingConverter = A.Fake<GlobalSettingConverter>();

            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            var fileTestWrapper = A.Fake<FileTestWrapper>();
            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new GlobalSettingsPersistenceService(xmlSerializerHelper, globalSettingConverter)
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            sut.Save(path, settingsEntries);

            // assert
            A.CallTo(() => directoryTestWrapper.CreateDirectory(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}