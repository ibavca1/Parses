using Chia.WebParsing.Companies.TehnoparkRu;
using Chia.WebParsing.Parsers.TehnoparkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoparkRu
{
    [TestClass]
    public class TehnoparkRuHtmlPageContentParserTest : TehnoparkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\main.mht", "TehnoparkRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\main.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.StPetersburg, TehnoparkRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<TehnoparkRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(TehnoparkRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(TehnoparkRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}