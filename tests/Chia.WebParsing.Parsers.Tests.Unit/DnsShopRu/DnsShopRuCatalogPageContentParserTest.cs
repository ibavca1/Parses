using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using Chia.WebParsing.Parsers.DnsShopRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.DnsShopRu
{
    [TestClass]
    public class DnsShopRuCatalogPageContentParserTest : DnsShopRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\catalog.mht", "DnsShopRu")]
        public void Test_Parse()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\catalog.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("197411", product.Article);
            Assert.AreEqual("Набор-конструктор Матрешка X $$ [Arduino Uno, 107 деталей]", product.Name);
            Assert.AreEqual(2290, product.OnlinePrice);
            Assert.AreEqual(2290, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/i197411/nabor-konstruktor-matreshka-x.html"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\catalog_out_of_stock.mht", "DnsShopRu")]
        public void Test_Parse_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\catalog.mht", "DnsShopRu")]
        public void Test_Parse_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\catalog.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new DnsShopRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(DnsShopRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.Product, "/catalog/i197411/nabor-konstruktor-matreshka-x.html", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\catalog_out_of_stock.mht", "DnsShopRu")]
        public void Test_Parse_DoNotGoToProductPageIfOutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] productPages = result.Pages.WhereType(DnsShopRuWebPageType.Product).ToArray();
            CollectionAssert.IsEmpty(productPages);
       }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\catalog_next_page.mht", "DnsShopRu")]
        public void Test_Parse_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(DnsShopRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, DnsShopRuWebPageType.Catalog, "/catalog/105/smartfony/?p=1", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\DnsShopRu\Pages\catalog_last_page.mht", "DnsShopRu")]
        public void Test_Parse_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"DnsShopRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, DnsShopRuCity.Chelyabinsk, DnsShopRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new DnsShopRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(DnsShopRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}