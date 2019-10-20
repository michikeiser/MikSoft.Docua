namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.GlobalSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture.NUnit3;

    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings;
    using MikSoft.Docua.Common.Data.FileSystem.GlobalSettings.XmlNodes;

    using NUnit.Framework;

    [TestFixture]
    internal class GlobalSettingConverterTests
    {
        [Test, AutoData]
        public void ToDocuaSettings_Default_ReturnCollection(IEnumerable<GlobalSettingsEntry> settingsEntries)
        {
            // arrange
            var sut = new GlobalSettingConverter();

            // act
            var result = sut.ToDocuaSettings(settingsEntries);

            // assert
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test, AutoData]
        public void ToSettingsEntries_Default_ReturnCollection(IEnumerable<DocuaGlobalSetting> userSettings)
        {
            // arrange
            var sut = new GlobalSettingConverter();

            // act
            var result = sut.ToSettingsEntries(userSettings);

            // assert
            Assert.That(result.Count(), Is.GreaterThan(0));
        }
    }
}