using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using Chia.WebParsing.Parsers.TelemaksRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TelemaksRu
{
    [TestClass]
    public class TelemaksRuCatalogPageContentParserTest : TelemaksRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\catalog.mht", "TelemaksRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\catalog.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual("23059603", product.Article);
            Assert.AreEqual("3D SMART UltraHD LED телевизор LG 55UB850V", product.Name);
            Assert.AreEqual(69990, product.OnlinePrice);
            Assert.AreEqual(69999, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/catalog/dep1460/position23059603/"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\catalog.mht", "TelemaksRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\catalog.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new TelemaksRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TelemaksRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TelemaksRuWebPageType.Product, "/catalog/dep1460/position23059603/", page.Path), productPage);            
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\catalog_next_page.mht", "TelemaksRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TelemaksRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TelemaksRuWebPageType.Catalog, "/catalog/dep1365/?p=2", page.Path), nextPage);            
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\catalog_last_page.mht", "TelemaksRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(TelemaksRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\catalog_empty.mht", "TelemaksRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\catalog_empty.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TelemaksRu\Pages\catalog_retail_only.mht", "TelemaksRu")]
        public void Test_ParseHtml_RetailOnly()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TelemaksRu\catalog_retail_only.mht");
            WebPage page = CreatePage(content, TelemaksRuCity.StPetersburg, TelemaksRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TelemaksRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.First();
            Assert.AreEqual(0, product.OnlinePrice);
            Assert.AreNotEqual(0, product.RetailPrice);
        }
    }
}