using System.Linq;
using Chia.WebParsing.Companies.TehnoparkRu;
using Chia.WebParsing.Parsers.TehnoparkRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.TehnoparkRu
{
    [TestClass]
    public class TehnoparkRuRazdelPageContentParserTest : TehnoparkRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\razdel.mht", "TehnoparkRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\razdel.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(TehnoparkRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoparkRuWebPageType.Razdel, "/dvd-i-blu-ray-pleery/", "DVD и Blu-ray плееры"), pages.First());
            Assert.AreEqual(CreatePage(page, TehnoparkRuWebPageType.Razdel, "/aksessuary-dlya-foto-video-kamer/", "Аксессуары для фото-видео камер"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\TehnoparkRu\Pages\catalog.mht", "TehnoparkRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"TehnoparkRu\catalog.mht");
            WebPage page = CreatePage(content, TehnoparkRuCity.Moscow, TehnoparkRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new TehnoparkRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(TehnoparkRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, TehnoparkRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);      
        }
    }
}