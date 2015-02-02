using System.Linq;
using Chia.WebParsing.Companies.NotikRu;
using Chia.WebParsing.Parsers.NotikRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NotikRu
{
    [TestClass]
    public class NotikRuCatalogPageContentParserTest : NotikRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\catalog.mht", "NotikRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\catalog.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Acer Extensa 2509 N2930 2Gb 500Gb Intel HD Graphics 15,6 HD DVD(DL) BT Cam 4700мАч Linux OS Черный 2509-C1NP NX.EEZER.002", product.Name);
            Assert.AreEqual(11290, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page,"/goods/36140.htm"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\catalog_empty.mht", "NotikRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\catalog_empty.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\catalog.mht", "NotikRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\catalog.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new NotikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(NotikRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(page.Site.MakeUri("/goods/36140.htm"), productPage.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\catalog.mht", "NotikRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\catalog.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(NotikRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, NotikRuWebPageType.Catalog, "http://m.notik.ru/search_catalog/filter/new.htm?page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NotikRu\Pages\catalog_last_page.mht", "NotikRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NotikRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, NotikRuCity.Moscow, NotikRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new NotikRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(NotikRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}