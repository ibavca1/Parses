using Chia.WebParsing.Companies.PultRu;
using Chia.WebParsing.Parsers.PultRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.PultRu
{
    [TestClass]
    public class PultRuHtmlPageContentParserTest : PultRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\main.mht", "PultRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\main.mht");
            WebPage page = CreatePage(content, PultRuCity.StPetersburg, PultRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<PultRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(PultRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(PultRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}