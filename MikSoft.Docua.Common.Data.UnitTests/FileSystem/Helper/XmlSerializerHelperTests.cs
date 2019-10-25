namespace MikSoft.Docua.Common.Data.UnitTests.FileSystem.Helper
{
    using System.Collections.Generic;

    using AutoFixture.NUnit3;

    using MikSoft.Docua.Common.Data.FileSystem.Helper;
    using MikSoft.Docua.Common.Data.FileSystem.UserSettings.XmlNodes;

    using NUnit.Framework;

    [TestFixture]
    internal class XmlSerializerHelperTests
    {
        [Test, AutoData]
        public void GetObject_Default_ReturnObject(DocuaUserSettingItems objectToSerialize)
        {
            // arrange
            var sut = new XmlSerializerHelper();
            var str = sut.Serialize(objectToSerialize);

            // act
            var docuaUserSettingItems = sut.Deserialize<DocuaUserSettingItems>(str);

            // assert
            Assert.That(docuaUserSettingItems, Is.Not.Null);
        }

        [Test, AutoData]
        public void GetString_Default_ReturnString(List<DocuaUserSettingItems> objectToSerialize)
        {
            // arrange
            var sut = new XmlSerializerHelper();

            // act
            var str = sut.Serialize(objectToSerialize);

            // assert
            Assert.That(str, Is.Not.Empty);
        }
    }
}