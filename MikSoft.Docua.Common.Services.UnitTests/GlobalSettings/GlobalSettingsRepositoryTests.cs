namespace MikSoft.Docua.Common.Services.UnitTests.GlobalSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.NUnit3;

    using FakeItEasy;

    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings;
    using MikSoft.Docua.Common.Services.GlobalSettings;
    using MikSoft.Docua.Common.Services.UserSettings;

    using NUnit.Framework;

    [TestFixture]
    internal class GlobalSettingsRepositoryTests
    {
        [Test, AutoData]
        public void AddAndSave_ExistingSetting_Save(string path, IList<GlobalSettingsEntry> sampleData)
        {
            // arrange
            var persistenceServiceMock = A.Fake<IGlobalSettingsPersistenceService>();
            A.CallTo(() => persistenceServiceMock.Load(path)).Returns(sampleData);

            var userSettingsRepositoryStub = A.Fake<IUserSettingsRepository>();
            A.CallTo(() => userSettingsRepositoryStub.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH)).Returns(path);

            var sut = new GlobalSettingsRepository(persistenceServiceMock, userSettingsRepositoryStub);

            // act
            var settingsEntry = sampleData.First();
            sut.AddAndSave(settingsEntry.Key, settingsEntry.Value);

            // assert
            A.CallTo(
                () => persistenceServiceMock.Save(
                    path,
                    A<List<GlobalSettingsEntry>>.That.Matches(
                        x => x.Single(y => y.Key == settingsEntry.Key && y.Value == settingsEntry.Value) != null))).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void AddAndSave_ExistingSetting_Save(string path)
        {
            // arrange
            var persistenceServiceMock = A.Fake<IGlobalSettingsPersistenceService>();
            A.CallTo(() => persistenceServiceMock.Load(path)).Returns(new List<GlobalSettingsEntry>());

            var userSettingsRepositoryStub = A.Fake<IUserSettingsRepository>();
            A.CallTo(() => userSettingsRepositoryStub.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH)).Returns(path);

            var sut = new GlobalSettingsRepository(persistenceServiceMock, userSettingsRepositoryStub);

            // act
            sut.AddAndSave("NOT_EXISTING_KEY", "NOT_EXISTING_VALUE");

            // assert
            A.CallTo(
                () => persistenceServiceMock.Save(
                    path,
                    A<List<GlobalSettingsEntry>>.That.Matches(
                        x => x.Single(y => y.Key == "NOT_EXISTING_KEY" && y.Value == "NOT_EXISTING_VALUE") != null))).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void AddAndSave_Save_CallPersistenceService(string path, string key, string value)
        {
            // arrange
            var persistenceServiceMock = A.Fake<IGlobalSettingsPersistenceService>();
            A.CallTo(() => persistenceServiceMock.Load(path)).Returns(new List<GlobalSettingsEntry>());

            var userSettingsRepositoryStub = A.Fake<IUserSettingsRepository>();
            A.CallTo(() => userSettingsRepositoryStub.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH)).Returns(path);

            var sut = new GlobalSettingsRepository(persistenceServiceMock, userSettingsRepositoryStub);

            // act
            sut.AddAndSave(key, value);

            // assert
            A.CallTo(
                    () => persistenceServiceMock.Save(
                        path,
                        A<List<GlobalSettingsEntry>>.That.Matches(x => x.Single(y => y.Key == key && y.Value == value) != null)))
                .MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Get_ExistingSetting_ReturnValue(string path, List<GlobalSettingsEntry> sampleData)
        {
            // arrange
            var persistenceServiceStub = A.Fake<IGlobalSettingsPersistenceService>();
            A.CallTo(() => persistenceServiceStub.Load(path)).Returns(sampleData);

            var userSettingsRepositoryStub = A.Fake<IUserSettingsRepository>();
            A.CallTo(() => userSettingsRepositoryStub.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH)).Returns(path);

            var sut = new GlobalSettingsRepository(persistenceServiceStub, userSettingsRepositoryStub);

            // act
            var settingsEntry = sampleData.First();
            var result = sut.Get(settingsEntry.Key);

            // assert
            Assert.That(result, Is.EqualTo(settingsEntry.Value));
        }

        [Test, AutoData]
        public void Get_Load_CallPersistenceService(string path)
        {
            // arrange
            var userSettingsPersistenceServiceMock = A.Fake<IGlobalSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceMock.Load(path)).Returns(new List<GlobalSettingsEntry>());

            var userSettingsRepositoryStub = A.Fake<IUserSettingsRepository>();
            A.CallTo(() => userSettingsRepositoryStub.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH)).Returns(path);

            var sut = new GlobalSettingsRepository(userSettingsPersistenceServiceMock, userSettingsRepositoryStub);
            sut.Get("NOT_EXISTING_KEY");

            // act + assert
            A.CallTo(() => userSettingsPersistenceServiceMock.Load(path)).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Get_MissingSetting_ReturnNull(string path)
        {
            // arrange
            var userSettingsPersistenceServiceStub = A.Fake<IGlobalSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceStub.Load(path)).Returns(new List<GlobalSettingsEntry>());

            var userSettingsRepositoryStub = A.Fake<IUserSettingsRepository>();
            A.CallTo(() => userSettingsRepositoryStub.Get(UserSettingsKeys.FILE_SYSTEM_REPOSITORY_PATH)).Returns(path);

            var sut = new GlobalSettingsRepository(userSettingsPersistenceServiceStub, userSettingsRepositoryStub);

            // act
            var result = sut.Get("NOT_EXISTING_KEY");

            // assert
            Assert.That(result, Is.Null);
        }
    }
}