using System.Linq;
using Chia.WebParsing.Companies.HolodilnikRu;
using Chia.WebParsing.Parsers.HolodilnikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.HolodilnikRu
{
    [TestClass]
    public class HolodilnikRuCatalogPageContentParserTest : HolodilnikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\catalog.mht", "HolodilnikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\catalog.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("176941", product.Article);
            Assert.AreEqual("Однокамерный холодильник Daewoo Electronics FR 081 AR", product.Name);
            Assert.AreEqual(6375, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page, "/refrigerator/one_chamber_refrigerators/daewoo/fr081ar/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\catalog_empty.mht", "HolodilnikRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\catalog_empty.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\catalog_out_of_stock.mht", "HolodilnikRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\catalog.mht", "HolodilnikRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\catalog.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(HolodilnikRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, HolodilnikRuWebPageType.Catalog, "/refrigerator/one_chamber_refrigerators/?page=2",page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\HolodilnikRu\Pages\catalog_last_page.mht", "HolodilnikRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"HolodilnikRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, HolodilnikRuCity.Moscow, HolodilnikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new HolodilnikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(HolodilnikRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}