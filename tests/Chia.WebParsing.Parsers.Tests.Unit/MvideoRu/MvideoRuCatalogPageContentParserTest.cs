using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using Chia.WebParsing.Parsers.MvideoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MvideoRu
{
    [TestClass]
    public class MvideoRuCatalogPageContentParserTest : MvideoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog.mht", "MvideoRu")]
        public void Test_ParseProduct()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Автомобильный усилитель (2 канала) Pioneer GM-A3602", product.Name);
            Assert.AreEqual(2990, product.OnlinePrice);
            Assert.AreEqual(2990, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/products/avtomobilnyi-usilitel-2-kanala-pioneer-gm-a3602-10005434"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog.mht", "MvideoRu")]
        public void Test_ParseProduct_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = new WebSiteParsingOptions {ProductArticle = true}};
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(MvideoRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Product, "/products/avtomobilnyi-usilitel-2-kanala-pioneer-gm-a3602-10005434", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog.mht", "MvideoRu")]
        public void Test_ParseProduct_GoToProductPageIfNeedAvailabiltyInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = new WebSiteParsingOptions {AvailabiltyInShops = true}};
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(MvideoRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Product, "/products/avtomobilnyi-usilitel-2-kanala-pioneer-gm-a3602-10005434", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog_out_of_stock.mht", "MvideoRu")]
        public void Test_ParseProduct_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog_unavailable_for_buy.mht", "MvideoRu")]
        public void Test_ParseProduct_UnavailableForBuy()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog_unavailable_for_buy.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog_unavailable_for_buy_on_site.mht", "MvideoRu")]
        public void Test_ParseProduct_UnavailableForBuyOnSite()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog_unavailable_for_buy_on_site.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();

            // act
            var parser = new MvideoRuCatalogPageContentParser();
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog_retail_only.mht", "MvideoRu")]
        public void Test_ParseProduct_RetailOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog_retail_only.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreNotEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog_long_name.mht", "MvideoRu")]
        public void Test_ParseProduct_GoToProductPageIfLongName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog_long_name.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();

            // act
            var parser = new MvideoRuCatalogPageContentParser();
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(MvideoRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Product, "/products/obektiv-dlya-zerkalnogo-fotoapparata-nikon-nikon-af-s-vr-micro-nikkor-105mm-f-2-8g-if-ed-10006860", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog_next_page.mht", "MvideoRu")]
        public void Test_ParseNextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(MvideoRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Catalog, "/audiotehnika-i-aksessuary/muzykalnye-centry-68/f/N-89r?Nrpp=24&Ns=product.weight%7C1%7C%7Cproduct.rating%7C1%7C%7Cproduct.reviewCount%7C1%7C%7Cproduct.reviewLastDate%7C1%7C%7Cproduct.displayName%7C0&No=24", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog_last_page.mht", "MvideoRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuCatalogPageContentParser();

            // act
            
            WebPageContentParsingResult result = parser.Parse(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(MvideoRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}