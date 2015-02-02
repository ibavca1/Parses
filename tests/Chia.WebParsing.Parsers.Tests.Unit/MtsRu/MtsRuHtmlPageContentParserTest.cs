using Chia.WebParsing.Companies.MtsRu;
using Chia.WebParsing.Parsers.MtsRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.MtsRu
{
    [TestClass]
    public class MtsRuHtmlPageContentParserTest : MtsRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MtsRu\Pages\main.mht", "MtsRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MtsRu\main.mht");
            WebPage page = CreatePage(content, MtsRuCity.Chelyabinsk, MtsRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<MtsRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(MtsRuCity.Chelyabinsk.Name, ex.Expected);
                Assert.AreEqual(MtsRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}