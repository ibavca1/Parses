using Chia.WebParsing.Companies.EurosetRu;
using Chia.WebParsing.Parsers.EurosetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.EurosetRu
{
    [TestClass]
    public class EurosetRuHtmlPageContentParserTest : EurosetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\main.mht", "EurosetRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\main.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<EurosetRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(EurosetRuCity.Moscow.Name, ex.Expected);
                Assert.AreEqual(EurosetRuCity.Chelyabinsk.Name, ex.Actual);
                throw;
            }
        }
    }
}