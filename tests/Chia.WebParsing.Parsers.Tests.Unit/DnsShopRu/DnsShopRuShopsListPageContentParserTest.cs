using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Parsers.DnsShopRu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseLibrary.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    [TestClass]
    public class DnsShopRuShopsListPageContentParserTest : DnsShopRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\main.mht", "DnsShopRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\main.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(DnsShopRuWebPageType.Shop).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.Shop, "/shop/chelyabinsk/tts+kol%60tso1", page.Path), pages.First());
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.Shop, "/shop/chelyabinsk/trk+fokus", page.Path), pages.Last());
        }
    }
}