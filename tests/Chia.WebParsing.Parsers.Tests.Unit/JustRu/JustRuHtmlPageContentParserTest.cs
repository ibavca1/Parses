using Chia.WebParsing.Companies.JustRu;
using Chia.WebParsing.Parsers.JustRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.JustRu
{
    [TestClass]
    public class JustRuHtmlPageContentParserTest : JustRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\JustRu\Pages\main.mht", "JustRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"JustRu\main.mht");
            WebPage page = CreatePage(content, JustRuCity.Moscow, JustRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<JustRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(JustRuCity.Moscow.Name, ex.Expected);
                Assert.AreEqual(JustRuCity.Chelyabinsk.Name, ex.Actual);
                throw;
            }
        }
    }
}