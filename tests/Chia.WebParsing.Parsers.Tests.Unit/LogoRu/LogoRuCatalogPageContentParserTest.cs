using System.Linq;
using Chia.WebParsing.Companies.LogoRu;
using Chia.WebParsing.Parsers.LogoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.LogoRu
{
    [TestClass]
    public class LogoRuCatalogPageContentParserTest : LogoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\catalog.mht", "LogoRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\catalog.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Пылесос Supra VCS 1400", product.Name);
            Assert.AreEqual(1090, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/shop/tehnika-dlya-doma/pylesosy/2431-supra-vcs-1400.html"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\catalog_out_of_stock.mht", "LogoRu")]
        public void Test_ParseHtml_OutOfStock()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\catalog_out_of_stock.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\catalog.mht", "LogoRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\catalog.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(LogoRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, LogoRuWebPageType.Catalog, "?page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\LogoRu\Pages\catalog_last_page.mht", "LogoRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"LogoRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, LogoRuCity.Ekaterinburg, LogoRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new LogoRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(LogoRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}