using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using Chia.WebParsing.Parsers.TechnonetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechnonetRu
{
    [TestClass]
    public class TechnonetRuCatalogPageContentParserTest : TechnonetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\catalog.mht", "TechnonetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\catalog.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Посудомоечная машина 60 см", product.Characteristics);
            Assert.AreEqual("Bosch SMS-40 D02 RU", product.Name);
            Assert.AreEqual(16410, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/G20652/78380/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\catalog_empty.mht", "TechnonetRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\catalog_empty.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\catalog_next_page.mht", "TechnonetRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TechnonetRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.Catalog, "/catalog/G10104/?page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\catalog_last_page.mht", "TechnonetRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TechnonetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(TechnonetRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechnonetRu\Pages\catalog.mht", "TechnonetRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechnonetRu\catalog.mht");
            WebPage page = CreatePage(content, TechnonetRuCity.Ufa, TechnonetRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TechnonetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TechnonetRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechnonetRuWebPageType.Product, "/catalog/G20652/78380/", page.Path), productPage);
        }
    }
}