using System.Linq;
using Chia.WebParsing.Companies.RbtRu;
using Chia.WebParsing.Parsers.RbtRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.RbtRu
{
    [TestClass]
    public class RbtRuRazdelContentPageParserTest : RbtRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\razdel.mht", "RbtRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\razdel.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Razdel);
            var context = new WebPageContentParsingContext();
            var parser = new RbtRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(RbtRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Razdel, "/cat/kuhonnaya_tehnika/holodilniki/", "Холодильники"), pages.First());
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Razdel, "/cat/kuhonnaya_tehnika/servirovka/", "Сервировка"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\RbtRu\Pages\catalog.mht", "RbtRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"RbtRu\catalog.mht");
            WebPage page = CreatePage(content, RbtRuCity.Chelyabinsk, RbtRuWebPageType.Razdel, "pagePath");
            var context = new WebPageContentParsingContext();
            var parser = new RbtRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.SingleWithType(RbtRuWebPageType.Catalog);
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, RbtRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}