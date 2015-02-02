using System;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebParsingFramework.Tests.Unit
{
    [TestClass]
    public class WebPageTest : UnitTest
    {
        [TestMethod]
        public void Test_GetUri()
        {
            // arrange
            var page = new WebPage(new Uri("http://localhost"), WebPageType.Unknown);

            // act
            Uri result = page.GetUri("/catalog/page");

            // assert
            Assert.AreEqual(new Uri("http://localhost/catalog/page"), result);
        }

        [TestMethod]
        public void Test_GetUri_WithParameters()
        {
            // arrange
            var page = new WebPage(new Uri("http://localhost"), WebPageType.Unknown);

            // act
            Uri result = page.GetUri("/catalog/page?p=10&a=20");

            // assert
            Assert.AreEqual(new Uri("http://localhost/catalog/page?p=10&a=20"), result);
        }

        [TestMethod]
        public void Test_GetUri_Absolute()
        {
            // arrange
            var page = new WebPage(new Uri("http://localhost"), WebPageType.Unknown);

            // act
            Uri result = page.GetUri("http://ya.ru");

            // assert
            Assert.AreEqual(new Uri("http://ya.ru"), result);
        }

        [TestMethod]
        public void Test_GetUri_OnlyParameters()
        {
            // arrange
            var page = new WebPage(new Uri("http://localhost"), WebPageType.Unknown);

            // act
            Uri result1 = page.GetUri("?p=10");
            Uri result2 = page.GetUri("p=10");

            // assert
            Assert.AreEqual(new Uri("http://localhost/?p=10"), result1);
            Assert.AreEqual(new Uri("http://localhost/?p=10"), result2);
        }
    }
}