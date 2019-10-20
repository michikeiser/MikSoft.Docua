namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.UserSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.NUnit3;

    using MikSoft.Docua.Common.Data.FileSystem.UserSettings;
    using MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes;

    using NUnit.Framework;

    [TestFixture]
    internal class UserSettingConverterTests
    {
        [Test, AutoData]
        public void ToSettingsEntries_Default_ReturnCollection(IEnumerable<DocuaUserSetting> userSettings)
        {
            // arrange
            var sut = new UserSettingConverter();

            // act
            var result = sut.ToSettingsEntries(userSettings);

            // assert
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test, AutoData]
        public void ToDocuaUserSettings_Default_ReturnCollection(IEnumerable<UserSettingsEntry> settingsEntries)
        {
            // arrange
            var sut = new UserSettingConverter();

            // act
            var result = sut.ToDocuaUserSettings(settingsEntries);

            // assert
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
    }
}