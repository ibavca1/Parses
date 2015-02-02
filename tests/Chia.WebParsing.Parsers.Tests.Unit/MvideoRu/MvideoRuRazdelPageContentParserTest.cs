using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using Chia.WebParsing.Parsers.MvideoRu;
using EnterpriseLibrary.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Tests.Unit.MvideoRu
{
    [TestClass]
    public class MvideoRuRazdelPageContentParserTest : MvideoRuWebPageContentParserUnitTest
    {
        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\razdel.mht", "MvideoRu")]
        public void Test_ParseHtml()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\razdel.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Razdel);
            var parser = new MvideoRuRazdelPageContentParser();
            var context = new WebPageContentParsingContext();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage[] pages = result.Pages.WhereType(MvideoRuWebPageType.Razdel).ToArray();
            CollectionAssert.IsNotEmpty(pages);
            CollectionAssert.All(pages, p => p.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Razdel, "/podvesy-dlya-televizorov/podvesy-naklonno-povorotnye-367", "Подвесы наклонно-поворотные"), pages.First());
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Razdel, "/podvesy-dlya-televizorov/podvesy-fiksirovannye-365", "Подвесы фиксированные"), pages.Last());
        }

        [TestMethod]
        [DeploymentItem(@"tests\Chia.WebParsing.Parsers.Tests.Unit\MvideoRu\Pages\catalog.mht", "MvideoRu")]
        public void Test_ParseHtml_CatalogPage()
        {
            // arrange
            MhtmlPageContent content = ReadMhtmlContent(@"MvideoRu\catalog.mht");
            WebPage page = CreatePage(content, MvideoRuCity.Moscow, MvideoRuWebPageType.Razdel, "path");
            var context = new WebPageContentParsingContext();
            var parser = new MvideoRuRazdelPageContentParser();

            // act
            WebPageContentParsingResult result = parser.ParseHtml(page, content, context);

            // assert
            WebPage catalogPage = result.Pages.Single();
            Assert.AreEqual(false, catalogPage.IsPartOfSiteMap);
            Assert.AreEqual(CreatePage(page, MvideoRuWebPageType.Catalog, page.Uri, page.Path), catalogPage);
        }
    }
}