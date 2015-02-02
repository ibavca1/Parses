using Chia.WebParsing.Companies.HolodilnikRu;
using Chia.WebParsing.Parsers.HolodilnikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.HolodilnikRu
{
    [TestClass]
    public class HolodilnikRuHtmlPageContentParserTest : HolodilnikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\main.mht", "HolodilnikRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\main.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.StPetersburg, HolodilnikRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<HolodilnikRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(HolodilnikRuCity.StPetersburg.Name, ex.Expected);
                Assert.AreEqual(HolodilnikRuCity.Moscow.Name, ex.Actual);
                throw;
            }
        }
    }
}