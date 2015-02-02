using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Parsers.UlmartRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    [TestClass]
    public class UlmartRuShopsListPageContentParserTest : UlmartRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\shops_list.mht", "UlmartRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\shops_list.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebShop[] shops = result.Shops.ToArray();
            CollectionAssert.IsNotEmpty(shops);
            Assert.AreEqual("г. Белгород, ул. Князя Трубецкого, д. 50.", shops.First().Name);
            Assert.AreEqual("г. Белгород, ул. Князя Трубецкого, д. 50.", shops.First().Address);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\shops_list_central_shops.mht", "UlmartRu")]
        public void Test_ParseHtml_CentralShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\shops_list_central_shops.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.ShopsList, "path");
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(UlmartRuWebPageType.Shop).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Shop, "http://www.ulmart.ru/help/moscow/msk_annino", page.Path), pages.First());
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Shop, "http://www.ulmart.ru/help/moscow/msk_schelk", page.Path), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\shops_list_single_central_shop.mht", "UlmartRu")]
        public void Test_ParseHtml_SingleCentralShop()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\shops_list_single_central_shop.mht");
            WebPage page = CreatePage(content, UlmartRuCity.RostovOnDon, UlmartRuWebPageType.ShopsList);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuShopsListPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage shopPage = result.Pages.SingleWithType(UlmartRuWebPageType.Shop);
            Assert.AreEqual(true, shopPage.IsPartOfShopsInformation);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Shop, "http://www.ulmart.ru/help/rostovnd/rnd_kosm", page.Path), shopPage);
        }
    }
}