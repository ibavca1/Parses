using Chia.WebParsing.Companies.OldiRu;
using Chia.WebParsing.Parsers.OldiRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.OldiRu
{
    [TestClass]
    public class OldiRuHtmlPageContentParserTest : OldiRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\main.mht", "OldiRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\main.mht");
            WebPage page = CreatePage(content, OldiRuCity.Syktyvkar, OldiRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<OldiRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(OldiRuCity.Syktyvkar.Name, ex.Expected);
                Assert.AreEqual(OldiRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}