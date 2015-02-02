using Chia.WebParsing.Companies.TechportRu;
using Chia.WebParsing.Parsers.TechportRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechportRu
{
    [TestClass]
    public class TechportRuHtmlPageContentParserTest : TechportRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\main.mht", "TechportRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\main.mht");
            WebPage page = CreatePage(content, TechportRuCity.StPetersburg, TechportRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<TechportRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(TechportRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(TechportRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}