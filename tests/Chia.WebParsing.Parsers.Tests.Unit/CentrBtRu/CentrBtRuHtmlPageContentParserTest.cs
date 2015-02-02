using Chia.WebParsing.Companies.CentrBtRu;
using Chia.WebParsing.Parsers.CentrBtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.CentrBtRu
{
    [TestClass]
    public class CentrBtRuHtmlPageContentParserTest : CentrBtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\main.mht", "CentrBtRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\catalog.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Ekaterinburg, CentrBtRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<CentrBtRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(CentrBtRuCity.Ekaterinburg.Name, ex.Expected);
                Assert.AreEqual(CentrBtRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}