using Chia.WebParsing.Companies.NotikRu;
using Chia.WebParsing.Parsers.NotikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.NotikRu
{
    [TestClass]
    public class NotikRuHtmlPageContentParserTest : NotikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\main.mht", "NotikRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\main.mht");
            WebPage page = CreatePage(content, NotikRuCity.StPetersburg, NotikRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<NotikRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(NotikRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(NotikRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}