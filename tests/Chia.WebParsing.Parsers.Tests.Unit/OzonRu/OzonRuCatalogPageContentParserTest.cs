using System.Linq;
using Chia.WebParsing.Companies;
using Chia.WebParsing.Companies.OzonRu;
using Chia.WebParsing.Parsers.OzonRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.OzonRu
{
    [TestClass]
    public class OzonRuCatalogPageContentParserTest : OzonRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\catalog.mht", "OzonRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(OzonRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Product, "/context/detail/id/23504961/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\catalog_empty.mht", "OzonRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\catalog_empty.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\catalog_categories.mht", "OzonRu")]
        public void Test_ParseHtml_Categories()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\catalog_categories.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(OzonRuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Catalog, "/catalog/1133740/", "Чайники"), pages.First());
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Catalog, "/catalog/1175043/", "Мини-печи"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\catalog.mht", "OzonRu")]
        public void Test_ParseHtml_NextSecondPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\catalog.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(OzonRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Catalog, "/catalog/1168060/?page=1", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\catalog_next_page.mht", "OzonRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(OzonRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, OzonRuWebPageType.Catalog, "/catalog/1168060/?page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\OzonRu\Pages\catalog_last_page.mht", "OzonRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"OzonRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, WebCities.Moscow, OzonRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new OzonRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleOrDefaultWithType(OzonRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}