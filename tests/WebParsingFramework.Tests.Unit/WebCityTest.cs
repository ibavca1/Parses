using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebParsingFramework.Tests.Unit
{
    [TestClass]
    public class WebCityTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            // arrange
            const int id = 10;
            const string name = "name";

            // act
            var city = new WebCity(id, name);

            // assert
            Assert.AreEqual(id, city.Id);
            Assert.AreEqual(name, city.Name);
        }
    }
}