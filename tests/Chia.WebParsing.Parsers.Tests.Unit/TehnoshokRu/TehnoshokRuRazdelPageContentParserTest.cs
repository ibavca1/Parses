using System.Linq;
using Chia.WebParsing.Companies.TehnoshokRu;
using Chia.WebParsing.Parsers.TehnoshokRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoshokRu
{
    [TestClass]
    public class TehnoshokRuRazdelPageContentParserTest : TehnoshokRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\razdel.mht", "TehnoshokRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\razdel.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TehnoshokRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.Razdel, "/tv_audio_video/lcd-televizory", "LED Телевизоры"), pages.First());
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.Razdel, "/group.html/801", "Hi-FI техника"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoshokRu\Pages\catalog.mht", "TehnoshokRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoshokRu\catalog.mht");
            WebPage page = CreatePage(content, TehnoshokRuCity.StPetersburg, TehnoshokRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoshokRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(TehnoshokRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoshokRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}