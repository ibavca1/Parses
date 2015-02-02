using System.Linq;
using Chia.WebParsing.Companies.NewmansRu;
using Chia.WebParsing.Parsers.NewmansRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.NewmansRu
{
    [TestClass]
    public class NewmansRuCatalogPageContentParserTest : NewmansRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\catalog.mht", "NewmansRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\catalog.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Монитор 24\" Dell U2412M", product.Name);
            Assert.AreEqual(14250, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(page.Site.MakeUri("/computers/display/133722.html"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\catalog.mht", "NewmansRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\catalog.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new NewmansRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(NewmansRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(page.Site.MakeUri("/computers/display/133722.html"), productPage.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\catalog_out_of_stock.mht", "NewmansRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions[6];
            Assert.AreEqual("Фотоаппарат Fujifilm FinePix S4800", product.Name);
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\catalog_empty_name.mht", "NewmansRu")]
        public void Test_ParseHtml_EmptyName()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\catalog_empty_name.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\catalog.mht", "NewmansRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\catalog.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(NewmansRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, NewmansRuWebPageType.Catalog, "/computers/display/page2/", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\NewmansRu\Pages\catalog_last_page.mht", "NewmansRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"NewmansRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, NewmansRuCity.Krasnodar, NewmansRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new NewmansRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(NewmansRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}