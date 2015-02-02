using Chia.WebParsing.Companies.TdPoiskRu;
using Chia.WebParsing.Parsers.TdPoiskRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.TdPoiskRu
{
    [TestClass]
    public class TdPoiskRuHtmlPageContentParserTest : TdPoiskRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TdPoiskRu\Pages\main.mht", "TdPoiskRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TdPoiskRu\main.mht");
            WebPage page = CreatePage(content, TdPoiskRuCity.RostovOnDon, TdPoiskRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<TdPoiskRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(TdPoiskRuCity.RostovOnDon.Name, ex.Expected);
                Assert.AreEqual(TdPoiskRuCity.Krasnodar.Name, ex.Actual);
                throw;
            }
        }
    }
}