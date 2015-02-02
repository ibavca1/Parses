using Chia.WebParsing.Companies.UtinetRu;
using Chia.WebParsing.Parsers.UtinetRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UtinetRu
{
    [TestClass]
    public class UtinetRuCatalogPageContentParserTest : UtinetRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\catalog.mht", "UtinetRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\catalog.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(UtinetRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UtinetRuWebPageType.Product, "http://utinet.ru/notebook/dell/precision/m3800/silver/3800-2274/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\catalog.mht", "UtinetRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\catalog.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(UtinetRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UtinetRuWebPageType.Catalog, "?offset=15&limit=15", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UtinetRu\Pages\catalog_last_page.mht", "UtinetRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UtinetRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, UtinetRuCity.Moscow, UtinetRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new UtinetRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(UtinetRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}