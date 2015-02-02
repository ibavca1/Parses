using Chia.WebParsing.Companies.CorpCentreRu;
using Chia.WebParsing.Parsers.CorpCentreRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.CorpCentreRu
{
    [TestClass]
    public class CorpCentreRuHtmlPageContentParserTest : CorpCentreRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CorpCentreRu\Pages\main.mht", "CorpCentreRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CorpCentreRu\razdel.mht");
            WebPage page = CreatePage(content, CorpCentreRuCity.Barnaul, CorpCentreRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<CorpCentreRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(CorpCentreRuCity.Barnaul.Name, ex.Expected);
                Assert.AreEqual(CorpCentreRuCity.Ekaterinburg.Name, ex.Actual);
                throw;
            }
        }
    }
}