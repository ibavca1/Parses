using Chia.WebParsing.Companies.TelemaksRu;
using Chia.WebParsing.Parsers.TelemaksRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.TelemaksRu
{
    [TestClass]
    public class TelemaksRuHtmlPageContentParserTest : TelemaksRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\main.mht", "TelemaksRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\main.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.Arhangelsk, TelemaksRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<TelemaksRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(TelemaksRuCity.Arhangelsk.Name, ex.Expected);
                Assert.AreEqual(TelemaksRuCity.StPetersburg.Name, ex.Actual);
                throw;
            }
        }
    }
}