using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Parsers.UlmartRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    [TestClass]
    public class UlmartRuHtmlPageContentParserTest : UlmartRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\main.mht", "UlmartRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\main.mht");
            WebPage page = CreatePage(content, UlmartRuCity.StPetersburg, UlmartRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<UlmartRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(UlmartRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(UlmartRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}