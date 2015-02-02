using Chia.WebParsing.Companies.EnterRu;
using Chia.WebParsing.Parsers.EnterRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.EnterRu
{
    [TestClass]
    public class EnterRuHtmlPageContentParserTest : EnterRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EnterRu\Pages\main.mht", "EnterRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EnterRu\main.mht");
            WebPage page = CreatePage(content, EnterRuCity.Chelyabinsk, EnterRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<EnterRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(EnterRuCity.Chelyabinsk.Name, ex.Expected);
                Assert.AreEqual(EnterRuCity.Petrozavodsk.Name, ex.Actual);
                throw;
            }
        }
    }
}