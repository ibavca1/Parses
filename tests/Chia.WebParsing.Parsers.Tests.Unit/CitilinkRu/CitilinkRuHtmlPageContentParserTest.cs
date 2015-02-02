using Chia.WebParsing.Companies.CitilinkRu;
using Chia.WebParsing.Parsers.CitilinkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.CitilinkRu
{
    [TestClass]
    public class CitilinkRuHtmlPageContentParserTest : CitilinkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CitilinkRu\Pages\main.mht", "CitilinkRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CitilinkRu\main.mht");
            WebPage page = CreatePage(content, CitilinkRuCity.StPetersburg, CitilinkRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<CitilinkRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(CitilinkRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(CitilinkRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}