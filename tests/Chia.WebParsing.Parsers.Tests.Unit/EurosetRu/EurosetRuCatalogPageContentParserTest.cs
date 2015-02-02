using System.Linq;
using Chia.WebParsing.Companies.EurosetRu;
using Chia.WebParsing.Parsers.EurosetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.EurosetRu
{
    [TestClass]
    public class EurosetRuCatalogPageContentParserTest : EurosetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\catalog.mht", "EurosetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\catalog.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Packard Bell ENTE69KB-12502G50Mnsk", product.Name);
            Assert.AreEqual(11490, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/computers/notebooks/packard-bell/-/packard-bell-ente69kb-12502g50mnsk/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\catalog_out_of_stock.mht", "EurosetRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\catalog_next_page.mht", "EurosetRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(EurosetRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, EurosetRuWebPageType.Catalog, "/catalog/computers/notebooks/?unset=true&section=ids167&PAGEN_1=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\catalog_last_page.mht", "EurosetRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(EurosetRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\EurosetRu\Pages\catalog_no_price.mht", "EurosetRu")]
        public void Test_ParseHtml_NoPrice()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"EurosetRu\catalog_no_price.mht");
            WebPage page = CreatePage(content, EurosetRuCity.Moscow, EurosetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new EurosetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }
    }
}