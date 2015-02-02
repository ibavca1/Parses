using Chia.WebParsing.Companies.OnlinetradeRu;
using Chia.WebParsing.Parsers.OnlinetradeRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.OnlinetradeRu
{
    [TestClass]
    public class OnlinetradeRuHtmlPageContentParserTest : OnlinetradeRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OnlinetradeRu\Pages\main.mht", "OnlinetradeRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OnlinetradeRu\main.mht");
            WebPage page = CreatePage(content, OnlinetradeRuCity.StPetersburg, OnlinetradeRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<OnlinetradeRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(OnlinetradeRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(OnlinetradeRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}