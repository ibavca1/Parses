using Chia.WebParsing.Companies.UtinetRu;
using Chia.WebParsing.Parsers.UtinetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.UtinetRu
{
    [TestClass]
    public class UtinetRuHtmlPageContentParserTest : UtinetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\main.mht", "UtinetRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\main.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Chelyabinsk, UtinetRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<UtinetRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(UtinetRuCity.Chelyabinsk.Name, ex.Expected);
                Assert.AreEqual(UtinetRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}