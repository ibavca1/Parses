using Chia.WebParsing.Companies.RbtRu;
using Chia.WebParsing.Parsers.RbtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.RbtRu
{
    [TestClass]
    public class RbtRuHtmlPageContentParserTest : RbtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\main.mht", "RbtRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\main.mht");
            WebPage page = CreatePage(content, RbtRuCity.Krasnoyarsk, RbtRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<RbtRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(RbtRuCity.Krasnoyarsk.Name, ex.Expected);
                Assert.AreEqual(RbtRuCity.Chelyabinsk.Name, ex.Actual);
                throw;
            }
        }
    }
}