using System.Linq;
using Chia.WebParsing.Companies.OldiRu;
using Chia.WebParsing.Parsers.OldiRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OldiRu
{
    [TestClass]
    public class OldiRuCatalogPageContentParserTest : OldiRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\catalog.mht", "OldiRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\catalog.mht");
            WebPage page = CreatePage(content, OldiRuCity.Ekaterinburg, OldiRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("0259273", product.Article);
            Assert.AreEqual("Ноутбук Lenovo IdeaPad G5030", product.Name);
            Assert.AreEqual("Celeron N2830 (2.16)/4G/320G/15.6\"HD GL/Int:Intel HD/No ODD/BT/Win8.1 (80G00096RK) (Black)", product.Characteristics);
            Assert.AreEqual(15048, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/element/0259273/"), product.Uri);
        }


        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\catalog_empty.mht", "OldiRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\catalog_empty.mht");
            WebPage page = CreatePage(content, OldiRuCity.Ekaterinburg, OldiRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\catalog.mht", "OldiRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedRetailPrice()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\catalog.mht");
            WebPage page = CreatePage(content, OldiRuCity.Ekaterinburg, OldiRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext {Options = {PriceTypes = WebPriceType.Retail}};
            var parser = new OldiRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(OldiRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreateUri(page, "/catalog/element/0259273/"), productPage.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\catalog.mht", "OldiRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\catalog.mht");
            WebPage page = CreatePage(content, OldiRuCity.Ekaterinburg, OldiRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new OldiRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(OldiRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreateUri(page, "/catalog/element/0259273/"), productPage.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\catalog.mht", "OldiRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\catalog.mht");
            WebPage page = CreatePage(content, OldiRuCity.Moscow, OldiRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(OldiRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OldiRuWebPageType.Catalog, "?PAGEN_1=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OldiRu\Pages\catalog_last_page.mht", "OldiRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OldiRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, OldiRuCity.Ekaterinburg, OldiRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OldiRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(OldiRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}