using Chia.WebParsing.Companies.TehnoshokRu;
using Chia.WebParsing.Parsers.TehnoshokRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoshokRu
{
    [TestClass]
    public class TehnoshokRuCatalogPageContentParserTest : TehnoshokRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\catalog.mht", "TehnoshokRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\catalog.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage productPage = result.Pages.FirstWithType(TehnoshokRuWebPageType.Product);
            Assert.AreEqual(false, productPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.Product, "card/18590/", page.Path), productPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\catalog_next_page.mht", "TehnoshokRu")]
        public void Test_ParseHtml_NextPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\catalog_next_page.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Catalog, "path");
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage nextPage = result.Pages.SingleWithType(TehnoshokRuWebPageType.Catalog);
            Assert.AreEqual(false, nextPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.Catalog, "/tv_audio_video/lcd-televizory?&page=2", page.Path), nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\catalog_last_page.mht", "TehnoshokRu")]
        public void Test_ParseHtml_LastPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\catalog_last_page.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Catalog);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
             WebPage nextPage = result.Pages.SingleOrDefaultWithType(TehnoshokRuWebPageType.Catalog);
            Assert.IsNull(nextPage);
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\catalog_empty.mht", "TehnoshokRu")]
        public void Test_ParseHtml_Empty()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\catalog_empty.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Catalog);
            
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuCatalogPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            Assert.IsTrue(result.IsEmpty);
        }
    }
}