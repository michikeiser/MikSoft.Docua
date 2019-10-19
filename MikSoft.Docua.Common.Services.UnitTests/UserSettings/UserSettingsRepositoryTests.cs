namespace MikSoft.Docua.Common.Services.UnitTests.UserSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.NUnit3;

    using FakeItEasy;

    using MikSoft.Docua.Common.Interfaces;
    using MikSoft.Docua.Common.Models;
    using MikSoft.Docua.Common.Services.UserSettings;

    using NUnit.Framework;

    [TestFixture]
    internal class UserSettingsRepositoryTests
    {
        [Test, AutoData]
        public void AddAndSave_ExistingSetting_Save(IEnumerable<SettingsEntry> sampleData)
        {
            // arrange
            var userSettingsPersistenceServiceMock = A.Fake<IUserSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceMock.Load()).Returns(sampleData);

            var sut = new UserSettingsRepository(userSettingsPersistenceServiceMock);

            // act
            var settingsEntry = sampleData.First();
            sut.AddAndSave(settingsEntry.Key, settingsEntry.Value);

            // assert
            A.CallTo(
                    () => userSettingsPersistenceServiceMock.Save(
                        A<List<SettingsEntry>>.That.Matches(
                            x => x.Single(y => y.Key == settingsEntry.Key && y.Value == settingsEntry.Value) != null)))
                .MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void AddAndSave_NotExistingSetting_Save()
        {
            // arrange
            var userSettingsPersistenceServiceMock = A.Fake<IUserSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceMock.Load()).Returns(new List<SettingsEntry>());

            var sut = new UserSettingsRepository(userSettingsPersistenceServiceMock);

            // act
            sut.AddAndSave("NOT_EXISTING_KEY", "NOT_EXISTING_VALUE");

            // assert
            A.CallTo(
                    () => userSettingsPersistenceServiceMock.Save(
                        A<List<SettingsEntry>>.That.Matches(
                            x => x.Single(y => y.Key == "NOT_EXISTING_KEY" && y.Value == "NOT_EXISTING_VALUE") != null)))
                .MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void AddAndSave_Save_CallPersistenceService(string key, string value)
        {
            // arrange
            var userSettingsPersistenceServiceMock = A.Fake<IUserSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceMock.Load()).Returns(new List<SettingsEntry>());

            var sut = new UserSettingsRepository(userSettingsPersistenceServiceMock);

            // act
            sut.AddAndSave(key, value);

            // assert
            A.CallTo(
                () => userSettingsPersistenceServiceMock.Save(
                    A<List<SettingsEntry>>.That.Matches(x => x.Single(y => y.Key == key && y.Value == value) != null))).MustHaveHappenedOnceExactly();
        }

        [Test, AutoData]
        public void Get_ExistingSetting_ReturnValue(IEnumerable<SettingsEntry> sampleData)
        {
            // arrange
            var userSettingsPersistenceServiceStub = A.Fake<IUserSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceStub.Load()).Returns(sampleData);

            var sut = new UserSettingsRepository(userSettingsPersistenceServiceStub);

            // act
            var settingsEntry = sampleData.First();
            var result = sut.Get(settingsEntry.Key);

            // assert
            Assert.That(result, Is.EqualTo(settingsEntry.Value));
        }

        [Test]
        public void Get_Load_CallPersistenceService()
        {
            // arrange
            var userSettingsPersistenceServiceMock = A.Fake<IUserSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceMock.Load()).Returns(new List<SettingsEntry>());

            var sut = new UserSettingsRepository(userSettingsPersistenceServiceMock);
            sut.Get("NOT_EXISTING_KEY");

            // act + assert
            A.CallTo(() => userSettingsPersistenceServiceMock.Load()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Get_MissingSetting_ReturnNull()
        {
            // arrange
            var userSettingsPersistenceServiceStub = A.Fake<IUserSettingsPersistenceService>();
            A.CallTo(() => userSettingsPersistenceServiceStub.Load()).Returns(new List<SettingsEntry>());

            var sut = new UserSettingsRepository(userSettingsPersistenceServiceStub);

            // act
            var result = sut.Get("NOT_EXISTING_KEY");

            // assert
            Assert.That(result, Is.Null);
        }
    }
}