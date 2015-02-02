using System.Linq;
using Chia.WebParsing.Companies.CentrBtRu;
using Chia.WebParsing.Parsers.CentrBtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.CentrBtRu
{
    [TestClass]
    public class CentrBtRuCatalogPageContentParserTest : CentrBtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\catalog.mht", "CentrBtRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\catalog.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new CentrBtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(null, product.Article);
            Assert.AreEqual("Стиральная машина AEG L 88489 FL 2", product.Name);
            Assert.AreEqual(35210, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreSame(CreateUri(page,"/index.php?goodid=0xJDTNDPPP"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\catalog.mht", "CentrBtRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedArticle()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\catalog.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext {Options = {ProductArticle = true}};
            var parser = new CentrBtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(CentrBtRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CentrBtRuWebPageType.Product,"/index.php?goodid=0xJDTNDPPP", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\catalog_next_page.mht", "CentrBtRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new CentrBtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(CentrBtRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, CentrBtRuWebPageType.Catalog, "/index.php?cat=0xRRJPPP&page=1", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\CentrBtRu\Pages\catalog_last_page.mht", "CentrBtRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"CentrBtRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, CentrBtRuCity.Moscow, CentrBtRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new CentrBtRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(CentrBtRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}