using Chia.WebParsing.Companies.ElectrovenikRu;
using Chia.WebParsing.Parsers.ElectrovenikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.ElectrovenikRu
{
    [TestClass]
    public class ElectrovenikRuHtmlPageContentParserTest : ElectrovenikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\ElectrovenikRu\Pages\main.mht", "ElectrovenikRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"ElectrovenikRu\main.mht");
            WebPage page = CreatePage(content, ElectrovenikRuCity.Chelyabinsk, ElectrovenikRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<ElectrovenikRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(ElectrovenikRuCity.Chelyabinsk.Name, ex.Expected);
                Assert.AreEqual(ElectrovenikRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}