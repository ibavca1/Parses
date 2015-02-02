using System.Linq;
using Chia.WebParsing.Companies.TechportRu;
using Chia.WebParsing.Parsers.TechportRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TechportRu
{
    [TestClass]
    public class TechportRuCatalogPageContentParserTest : TechportRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\catalog_categories.mht", "TechportRu")]
        public void Test_ParseHtml_Categories()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\catalog_categories.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TechportRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechportRuWebPageType.Catalog, "katalog/products/holodilniki", "Холодильники"), pages.First());
            Assert.AreEqual(CreatePage(page, TechportRuWebPageType.Catalog, "katalog/products/otdelnostojaschaja-bytovaja-tehnika/plity", "Плиты"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\catalog.mht", "TechportRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\catalog.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("id3363", product.Article);
            Assert.AreEqual("Холодильник Indesit TT 85", product.Name);
            Assert.AreEqual(8690, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page,"/katalog/products/holodilniki/odnokamernye/holodilnik-indesit-tt-85"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\catalog_empty.mht", "TechportRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\catalog_empty.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\catalog_out_of_stock.mht", "TechportRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\catalog.mht", "TechportRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\catalog.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TechportRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TechportRuWebPageType.Catalog, "/katalog/products/holodilniki/odnokamernye?offset=20", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TechportRu\Pages\catalog_last_page.mht", "TechportRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TechportRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TechportRuCity.Moscow, TechportRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TechportRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(TechportRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}