using System.Linq;
using Chia.WebParsing.Companies.Oo3Ru;
using Chia.WebParsing.Parsers.Oo3Ru;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.Oo3Ru
{
    [TestClass]
    public class Oo3RuCatalogPageContentParserTest : Oo3RuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\catalog_categories.mht", "Oo3Ru")]
        public void Test_ParseHtml_Categories()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\catalog_categories.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(Oo3RuWebPageType.Catalog).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Oo3RuWebPageType.Catalog, "/catalog-3005933.html", "Холодильники, морозильники"), pages.First());
            Assert.AreEqual(CreatePage(page, Oo3RuWebPageType.Catalog, "/catalog-3004051.html", "Аксессуары для крупной бытовой техники"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\catalog.mht", "Oo3Ru")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\catalog.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstOrDefaultWithType(Oo3RuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Oo3RuWebPageType.Product, "/product-256305733.html", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\catalog.mht", "Oo3Ru")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\catalog.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(Oo3RuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, Oo3RuWebPageType.Catalog, "/catalog-3005961-2.html", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\Oo3Ru\Pages\catalog_last_page.mht", "Oo3Ru")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"Oo3Ru\catalog_last_page.mht");
            WebPage page = CreatePage(content, Oo3RuCity.Moscow, Oo3RuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new Oo3RuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.FirstOrDefaultWithType(Oo3RuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }
    }
}