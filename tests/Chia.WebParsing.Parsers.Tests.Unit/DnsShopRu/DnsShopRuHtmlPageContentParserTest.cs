using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Parsers.DnsShopRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    [TestClass]
    public class DnsShopRuHtmlPageContentParserTest : DnsShopRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\main.mht", "DnsShopRu")]
        [ExpectedException(typeof(InvalidWebCityException))]
        public void Test_ValidateCity()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\main.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Krasnoyarsk, DnsShopRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new Mock<DnsShopRuHtmlPageContentParser> { CallBase = true }.Object;

            // act
            try
            {
                WebPageContentParsingResult result = parser.Parse(page, content, context);
                Assert.IsNotNull(result);
            }
            catch (InvalidWebCityException ex)
            {
                Assert.AreEqual(DnsShopRuCity.Krasnoyarsk.Name, ex.Expected);
                Assert.AreEqual(DnsShopRuCity.Chelyabinsk.Name, ex.Actual);
                throw;
            }
        }
    }
}