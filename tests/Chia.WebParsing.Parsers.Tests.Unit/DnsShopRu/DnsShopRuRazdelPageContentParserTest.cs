using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Parsers.DnsShopRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    [TestClass]
    public class DnsShopRuRazdelPageContentParserTest : DnsShopRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\razdel.mht", "DnsShopRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\razdel.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(DnsShopRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.Catalog, "/catalog/44/noutbuki/", "Ноутбуки, компьютеры и программное обеспечение", "Ноутбуки"), pages.First());
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.Catalog, "/catalog/432/prochaya-texnika-dlya-doma/", "Климат и техника для дома", "Прочая техника для дома"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\razdel_empty_name.mht", "DnsShopRu")]
        public void Test_Parse_EmptyName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\razdel_empty_name.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            CollectionAssert.IsNotEmpty(result.Pages.ToArray());}
    }
}