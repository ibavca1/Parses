using System.Linq;
using Chia.WebParsing.Companies.EldoradoRu;
using Chia.WebParsing.Parsers.EldoradoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EldoradoRu
{
    [TestClass]
    public class EldoradoRuCatalogPageContentParserTest : EldoradoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog.mht", "EldoradoRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("LED телевизор THOMSON T32E02U", product.Name);
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual(9199, product.OnlinePrice);
            Assert.AreEqual(9199, product.RetailPrice);
            Assert.AreSame(CreateUri(page, "/cat/detail/71096212/?category=1482093"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog_out_of_stock.mht", "EldoradoRu")]
        public void Test_Parse_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog_only_in_shops.mht", "EldoradoRu")]
        public void Test_Parse_OnlyInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog_only_in_shops.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog_comming_soon.mht", "EldoradoRu")]
        public void Test_Parse_CommingSoon()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog_comming_soon.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog.mht", "EldoradoRu")]
        public void Test_Parse_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext { Options = { AvailabiltyInShops = true } };
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(EldoradoRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.Product, "/cat/detail/71096212/?category=1482093", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog_next_page.mht", "EldoradoRu")]
        public void Test_Parse_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstWithType(EldoradoRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EldoradoRuWebPageType.Catalog, "cat/1482093/page/2/", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog_last_page.mht", "EldoradoRu")]
        public void Test_Parse_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(EldoradoRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog_empty.mht", "EldoradoRu")]
        public void Test_Parse_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog_empty.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Moscow, EldoradoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EldoradoRu\Pages\catalog_empty_2.mht", "EldoradoRu")]
        public void Test_Parse_Empty2()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EldoradoRu\catalog_empty_2.mht");
            WebPage page = CreatePage(content, EldoradoRuCity.Belgorod, EldoradoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EldoradoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }
    }
}