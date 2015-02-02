using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Parsers.DnsShopRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    [TestClass]
    public class DnsShopRuMainPageContentParserTest : DnsShopRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\main.mht", "DnsShopRu")]
        public void Test_ParseHtml_RazdelPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\main.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Main);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage razdelPage = result.Pages.SingleWithType(DnsShopRuWebPageType.Razdel);
            Assert.AreEqual(true, razdelPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.Razdel, "/catalog/", page.Path), razdelPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\main.mht", "DnsShopRu")]
        public void Test_ParseHtml_ShopsPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\main.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Main);
            var context = new WebPageContentParsingContext {Options = {InformationAboutShops = true}};
            var parser = new DnsShopRuMainPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopsPage = result.Pages.SingleWithType(DnsShopRuWebPageType.ShopsList);
            Assert.AreEqual(true, shopsPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.ShopsList, page.Uri, page.Path), shopsPage);
        }
    }
}