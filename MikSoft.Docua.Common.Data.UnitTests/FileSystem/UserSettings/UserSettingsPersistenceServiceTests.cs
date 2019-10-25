namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.UserSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.NUnit3;

    using FakeItEasy;

    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.Data.FileSystem.UserSettings;
    using MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes;
    using MikSoft.Docua.Common.TestWrappers;

    using NUnit.Framework;

    [TestFixture]
    internal class UserSettingsPersistenceServiceTests
    {
        [Test, AutoData]
        public void Load_FileExisting_ReturnCallConverter()
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var userSettingConverter = A.Fake<UserSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(true);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new UserSettingsPersistenceService(xmlSerializerHelper, userSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load();

            // assert
            A.CallTo(() => userSettingConverter.ToSettingsEntries(A<IEnumerable<DocuaUserSetting>>.That.Not.IsEmpty()))
                .MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Load_FileExisting_ReturnCallFileTestWrapper()
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var userSettingConverter = A.Fake<UserSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(true);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new UserSettingsPersistenceService(xmlSerializerHelper, userSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load();

            // assert
            A.CallTo(() => fileTestWrapper.ReadAllText(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Load_FileExisting_ReturnCallSerializer()
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var userSettingConverter = A.Fake<UserSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(true);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new UserSettingsPersistenceService(xmlSerializerHelper, userSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load();

            // assert
            A.CallTo(() => xmlSerializerHelper.Deserialize<DocuaUserSettingItems>(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Load_FileNotExists_ReturnEmptyList()
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var userSettingConverter = A.Fake<UserSettingConverter>();

            var fileTestWrapper = A.Fake<FileTestWrapper>();
            A.CallTo(() => fileTestWrapper.Exists(A<string>.Ignored)).Returns(false);

            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new UserSettingsPersistenceService(xmlSerializerHelper, userSettingConverter)
                          {
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            var result = sut.Load();

            // assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test, AutoData]
        public void Save_DirectoryExists_CallConverter(IEnumerable<UserSettingsEntry> settingsEntries)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var userSettingConverter = A.Fake<UserSettingConverter>();

            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            var fileTestWrapper = A.Fake<FileTestWrapper>();
            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new UserSettingsPersistenceService(xmlSerializerHelper, userSettingConverter)
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            sut.Save(settingsEntries);

            // assert
            A.CallTo(() => userSettingConverter.ToDocuaSettings(A<IEnumerable<UserSettingsEntry>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Save_DirectoryNotExists_CallCreateDirectory(IEnumerable<UserSettingsEntry> settingsEntries)
        {
            // arrange
            var xmlSerializerHelper = A.Fake<XmlSerializerHelper>();

            var userSettingConverter = A.Fake<UserSettingConverter>();

            var directoryTestWrapper = A.Fake<DirectoryTestWrapper>();
            var fileTestWrapper = A.Fake<FileTestWrapper>();
            var pathTestWrapper = A.Fake<PathTestWrapper>();

            var sut = new UserSettingsPersistenceService(xmlSerializerHelper, userSettingConverter)
                          {
                              DirectoryTestWrapper = directoryTestWrapper,
                              FileTestWrapper = fileTestWrapper,
                              PathTestWrapper = pathTestWrapper
                          };

            // act
            sut.Save(settingsEntries);

            // assert
            A.CallTo(() => directoryTestWrapper.CreateDirectory(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}