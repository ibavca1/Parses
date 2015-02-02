using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using Chia.WebParsing.Parsers.UlmartRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.UlmartRu
{
    [TestClass]
    public class UlmartRuCatalogPageContentParserTest : UlmartRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\catalog.mht", "UlmartRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\catalog.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebMonitoringPosition product = result.Positions.Single();
            Assert.AreEqual("413129", product.Article);
            Assert.AreEqual("электрошинковка Smile SM 2711", product.Name);
            Assert.AreEqual("электрошинковка Smile SM 2711, 45 Вт", product.Characteristics);
            Assert.AreEqual(2230, product.OnlinePrice);
            Assert.AreEqual(0, product.RetailPrice);
            Assert.AreEqual(CreateUri(page, "/goods/413129"), product.Uri);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\catalog_empty.mht", "UlmartRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\catalog_empty.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.AreEqual(true, result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\catalog.mht", "UlmartRu")]
        public void Test_ParseHtml_GoToProductPageIfNeedAvailabilityInShops()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\catalog.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext {Options = {AvailabiltyInShops = true}};
            var parser = new UlmartRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.SingleOrDefaultWithType(UlmartRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(page.Path, productPage.Path);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Product, "/goods/413129", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\catalog.mht", "UlmartRu")]
        public void Test_ParseHtml_SecondPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\catalog.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(UlmartRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Catalog, "?pageNum=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\UlmartRu\Pages\catalog_next_page.mht", "UlmartRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"UlmartRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, UlmartRuCity.Moscow, UlmartRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new UlmartRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(UlmartRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, UlmartRuWebPageType.Catalog, "?pageNum=3", page.Path), nextPage);
        }
    }
}