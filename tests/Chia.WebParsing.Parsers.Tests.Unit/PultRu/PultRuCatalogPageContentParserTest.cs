using System.Linq;
using Chia.WebParsing.Companies.PultRu;
using Chia.WebParsing.Parsers.PultRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.PultRu
{
    [TestClass]
    public class PultRuCatalogPageContentParserTest : PultRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\catalog.mht", "PultRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\catalog.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Комплект акустики Yamaha NS-P60 MKII cherry", product.Name);
            Assert.AreEqual(3200, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page, "/product/50097.htm"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\catalog.mht", "PultRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\catalog.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new PultRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstOrDefaultWithType(PultRuWebPageType.Product);
            Assert.AreEqual(CreatePage(page, PultRuWebPageType.Product, "/product/50097.htm", page.Path), productPage);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\catalog.mht", "PultRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\catalog.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new PultRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(PultRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, PultRuWebPageType.Catalog, "/product/akusticheskie-sistemy-akustika/komplekty-akustiki/?PAGEN_1=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\PultRu\Pages\catalog_last_page.mht", "PultRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"PultRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, PultRuCity.Moscow, PultRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new PultRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(PultRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}