using System.Collections.Generic;
using System.IO;
using Chia.WebParsing.Companies.EldoradoRu;
using Chia.WebParsing.Parsers.EldoradoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.EldoradoRu
{
    [TestClass]
    public class EldoradoRuHtmlPageContentParserTest : EldoradoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\main.mht", "EldoradoRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\main.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Cheboksary, EldoradoRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<EldoradoRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(EldoradoRuCity.Cheboksary.Name, ex.Expected);
                Assert.AreEqual(EldoradoRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\prices_json.txt", "EldoradoRu")]
        public void Test_ParseProductPrices()
        {
            // arrange
            string json = File.ReadAllText(@"EldoradoRu\prices_json.txt");
            var content = new StringWebPageContent(json);

            // act
            var parser = new Mock<EldoradoRuHtmlPageContentParser> { CallBase = true }.Object;
            IDictionary<string, decimal> result = parser.ParseProductPrices(content);

            // assert
            Assert.AreEqual(0, result["5320453"]);
            Assert.AreEqual(0, result["7977586"]);
            Assert.AreEqual(0, result["158054251"]);
            Assert.AreEqual(3699, result["158054257"]);
        }
    }
}